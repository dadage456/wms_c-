import 'dart:async';

import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:hive/hive.dart';

import '../models/barcode_content.dart';
import '../models/mat_control_info.dart';
import '../models/outbound_collected_summary.dart';
import '../models/outbound_collection_record.dart';
import '../models/outbound_collection_repository.dart';
import '../models/outbound_down_shelves_payload.dart';
import '../models/outbound_inventory_record.dart';
import '../models/outbound_task_item.dart';

part 'outbound_collection_event.dart';
part 'outbound_collection_state.dart';
enum _ScanStep { barcode2d, site, quantity }

class _OperationQuantities {
  _OperationQuantities({required this.taskQty, required this.collectedQty});

  final double taskQty;
  final double collectedQty;
}
class OutboundCollectionBloc
    extends Bloc<OutboundCollectionEvent, OutboundCollectionState> {
  OutboundCollectionBloc({
    required this.repository,
    required this.taskNo,
    required this.taskId,
    required this.taskComment,
    required this.workStation,
    required this.storeRoom,
    required this.siteFlag,
    required this.batchFlag,
    required this.userId,
    required this.roleId,
    required this.roomTag,
    required Future<Box<OutboundCollectionRecord>> localBox,
  })  : _localBoxFuture = localBox,
        super(OutboundCollectionState.initial(
          storeRoom: storeRoom,
          siteFlag: siteFlag,
          batchFlag: batchFlag,
        )) {
    on<_OutboundCollectionInitialized>(_onInitialized);
    on<OutboundCollectionBarcodeScanned>(_onBarcodeScanned);
    on<OutboundCollectionSubmitRequested>(_onSubmitRequested);
    on<OutboundCollectionPendingActionConfirmed>(
        _onPendingActionConfirmed);
    on<OutboundCollectionCloseRequested>(_onCloseRequested);
    on<OutboundCollectionSelectionToggled>(_onSelectionToggled);
    on<OutboundCollectionFinishSelected>(_onFinishSelected);
    on<OutboundCollectionSuccessCleared>(_onSuccessCleared);
    on<OutboundCollectionErrorCleared>(_onErrorCleared);
    on<OutboundCollectionDetailOpened>(_onDetailOpened);
    on<OutboundCollectionExceptionOpened>(_onExceptionOpened);
    on<OutboundCollectionSupplierOpened>(_onSupplierOpened);
    on<OutboundCollectionNavigationCleared>(_onNavigationCleared);
    on<OutboundCollectionCloseAcknowledged>(_onCloseAcknowledged);

    add(const _OutboundCollectionInitialized());
  }

  final OutboundCollectionRepository repository;
  final String taskNo;
  final String taskId;
  final String taskComment;
  final String workStation;
  final String storeRoom;
  final String siteFlag;
  final String batchFlag;
  final String userId;
  final String roleId;
  final String roomTag;
  final Future<Box<OutboundCollectionRecord>> _localBoxFuture;
  Box<OutboundCollectionRecord>? _localBox;

  Future<void> _onInitialized(
    _OutboundCollectionInitialized event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    await _initialize();
  }

  Future<void> _onBarcodeScanned(
    OutboundCollectionBarcodeScanned event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    await _handleBarcodeScanned(event.raw);
  }

  Future<void> _onSubmitRequested(
    OutboundCollectionSubmitRequested event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    await _submitCollection();
  }

  Future<void> _onPendingActionConfirmed(
    OutboundCollectionPendingActionConfirmed event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    await _confirmPendingAction(event.confirmed);
  }

  Future<void> _onCloseRequested(
    OutboundCollectionCloseRequested event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _requestClose();
  }

  Future<void> _onSelectionToggled(
    OutboundCollectionSelectionToggled event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _toggleSelection(event.outTaskItemId);
  }

  Future<void> _onFinishSelected(
    OutboundCollectionFinishSelected event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    await _finishSelected();
  }

  Future<void> _onSuccessCleared(
    OutboundCollectionSuccessCleared event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _clearSuccessMessage();
  }

  Future<void> _onErrorCleared(
    OutboundCollectionErrorCleared event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _clearErrorMessage();
  }

  Future<void> _onDetailOpened(
    OutboundCollectionDetailOpened event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _openCollectionDetail();
  }

  Future<void> _onExceptionOpened(
    OutboundCollectionExceptionOpened event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _openExceptionTask();
  }

  Future<void> _onSupplierOpened(
    OutboundCollectionSupplierOpened event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _openSupplierTask();
  }

  Future<void> _onNavigationCleared(
    OutboundCollectionNavigationCleared event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _clearNavigation();
  }

  Future<void> _onCloseAcknowledged(
    OutboundCollectionCloseAcknowledged event,
    Emitter<OutboundCollectionState> emit,
  ) async {
    _acknowledgeCloseHandled();
  }
  Future<void> _initialize() async {
    emit(state.copyWith(status: OutboundCollectionStatus.loading));
    try {
      if (!Hive.isAdapterRegistered(OutboundCollectionRecordAdapter().typeId)) {
        Hive.registerAdapter(OutboundCollectionRecordAdapter());
      }
    } catch (_) {
      // ignore duplicate registration errors
    }

    try {
      _localBox ??= await _localBoxFuture;
      final String roomMatControl = await repository.getRoomMatControl(taskId);
      final List<OutboundTaskItem> items = await repository.fetchTaskItems(
        taskNo: taskNo,
        userId: userId,
        roleId: roleId,
        workStation: workStation,
        roomTag: roomTag,
        taskComment: taskComment,
      );
      final List<OutboundCollectionRecord> local = _localBox!.values.toList();
      emit(state.copyWith(
        status: OutboundCollectionStatus.ready,
        taskItems: items,
        filteredItems: items,
        roomMatControl: roomMatControl,
        collectionRecords: local,
        messageLabel: '请扫描库位',
      ));
    } catch (error) {
      emit(state.copyWith(
        status: OutboundCollectionStatus.failure,
        errorMessage: error.toString(),
      ));
    }
  }
  bool _isQuantity(String input) {
    final double? value = double.tryParse(input);
    if (value == null) {
      return false;
    }
    if (value <= 0 || value > 999999) {
      return false;
    }
    final List<String> parts = input.split('.');
    if (parts.length > 2) {
      return false;
    }
    bool _isDigits(String text) {
      for (final int code in text.codeUnits) {
        if (code < 48 || code > 57) {
          return false;
        }
      }
      return text.isNotEmpty;
    }

    if (!_isDigits(parts.first)) {
      return false;
    }
    if (parts.length == 2) {
      if (!_isDigits(parts[1]) || parts[1].length > 7) {
        return false;
      }
    }
    return true;
  }
  Future<void> _handleBarcodeScanned(String raw) async {
    if (state.status != OutboundCollectionStatus.ready) {
      return;
    }
    final String barcode = raw.trim();
    if (barcode.isEmpty) {
      emit(state.copyWith(errorMessage: '条码不能为空'));
      return;
    }
    _ScanStep step;
    if (barcode.contains('*')) {
      step = _ScanStep.barcode2d;
    } else if (barcode.startsWith(r'$KW$')) {
      step = _ScanStep.site;
    } else if (_isQuantity(barcode)) {
      step = _ScanStep.quantity;
    } else {
      emit(state.copyWith(errorMessage: _buildMessage('条码格式错误,')));
      return;
    }

    try {
      switch (step) {
        case _ScanStep.barcode2d:
          await _handle2DBarcode(barcode);
          break;
        case _ScanStep.site:
          await _handleSiteBarcode(barcode);
          break;
        case _ScanStep.quantity:
          _handleQuantity(barcode);
          break;
      }
      final String message = _buildMessage('');
      if (message.trim().isEmpty) {
        final double qty = state.quantityInput ?? 0;
        await _dealQuantity(qty, state.matControlFlag);
        _initializeCollect();
      }
      emit(state.copyWith(
        messageLabel: _buildMessage(''),
        errorMessage: null,
      ));
    } catch (error) {
      emit(state.copyWith(
        errorMessage: error.toString(),
        messageLabel: _buildMessage(''),
      ));
    }
  }
  Future<void> _handle2DBarcode(String barcode) async {
    final BarcodeContent barcodeContent =
        await repository.analysisBarcode(barcode);
    final bool isNewMatTask = barcode.contains('*BN');

    final MatControlInfo matControlInfo =
        await repository.getMatControl(barcodeContent.matCode);

    String matSendControl = matControlInfo.sendControl.isEmpty
        ? '0'
        : matControlInfo.sendControl;

    String serialNumber = '';
    String batchNo = '';
    double qty = 1;
    String matCode = barcodeContent.matCode;
    String matInnerCode = '';
    String matName = '';
    final int matControl = matControlInfo.controlType;

    if (!isNewMatTask) {
      if (matControl == 0) {
        if (barcodeContent.sn.isEmpty) {
          throw Exception('物料$matCode序列号为空，无法采集');
        }
        if (state.scannedSerials
            .containsKey('${barcodeContent.matCode}@${barcodeContent.sn}')) {
          throw Exception('序列号${barcodeContent.sn}已经采集');
        }
        serialNumber = barcodeContent.sn;
        batchNo = '';
        qty = 1;
        for (final OutboundCollectionRecord record in state.collectionRecords) {
          if (record.sn == barcodeContent.sn) {
            throw Exception(
              '物料${barcodeContent.matCode}序列号${barcodeContent.sn}在库位${record.storeSite}已采集,禁止重复采集',
            );
          }
        }
      } else if (matControl == 1 || matControl == 2) {
        if ((matSendControl == '0' && state.roomMatControl == '0') ||
            state.roomMatControl == '1') {
          await _checkMat(barcodeContent.matCode, barcodeContent.batchNo);
          await _checkMtlSite(
            state.storeSite,
            barcodeContent.sn,
            barcodeContent.matCode,
            matControl.toString(),
          );
        }
        serialNumber = '';
        batchNo = barcodeContent.sn;
      } else {
        throw Exception('物料$matCode控制属性非法');
      }
    } else {
      if (matControl == 0) {
        if (barcodeContent.sn.isEmpty) {
          throw Exception('物料$matCode序列号为空，无法采集');
        }
        serialNumber = barcodeContent.sn;
        batchNo = barcodeContent.batchNo.isEmpty
            ? barcodeContent.sn
            : barcodeContent.batchNo;
        qty = 1;
        if (state.scannedSerials
            .containsKey('${barcodeContent.matCode}@${barcodeContent.sn}')) {
          throw Exception('序列号${barcodeContent.sn}已经采集');
        }
      } else if (matControl == 1 || matControl == 2) {
        if ((matSendControl == '0' && state.roomMatControl == '0') ||
            state.roomMatControl == '1') {
          await _checkMat(barcodeContent.matCode, barcodeContent.batchNo);
          await _checkMtlSite(
            state.storeSite,
            barcodeContent.batchNo,
            barcodeContent.matCode,
            matControl.toString(),
          );
        }
        batchNo = barcodeContent.batchNo;
        serialNumber = '';
      } else {
        throw Exception('物料$matCode控制属性非法');
      }
    }

    for (final OutboundTaskItem item in state.taskItems) {
      if (item.matCode == matCode) {
        matInnerCode = item.matInnerCode;
        matName = item.matName;
        break;
      }
    }

    final List<OutboundTaskItem> filtered = _queryTask(state.storeSite, matCode);

    emit(state.copyWith(
      matCode: matCode,
      matInnerCode: matInnerCode,
      matName: matName,
      matSendControl: matSendControl,
      matControlFlag: matControl.toString(),
      sn: serialNumber,
      batchNo: batchNo,
      quantityInput: qty,
      filteredItems: filtered,
      errorMessage: null,
    ));

    await _checkInventory(0);
  }
  Future<void> _handleSiteBarcode(String barcode) async {
    const String prefix = r'$KW$';
    if (!barcode.startsWith(prefix) || barcode.length <= prefix.length) {
      throw Exception('库位条码格式错误');
    }
    final String site = barcode.substring(prefix.length);
    await repository.validateStoreSite(storeRoom: storeRoom, storeSite: site);
    if ((state.matSendControl == '0' && state.roomMatControl == '0') ||
        state.roomMatControl == '1') {
      await _checkMtlSite(site, state.batchNo, state.matCode, state.matControlFlag);
    }
    final List<OutboundTaskItem> filtered = _queryTask(site, state.matCode);
    emit(state.copyWith(
      storeSite: site,
      filteredItems: filtered,
      errorMessage: null,
    ));
    await _checkInventory(0);
  }
  void _handleQuantity(String barcode) {
    if (state.sn.isNotEmpty) {
      throw Exception('序列号物料数量固定为1，请勿手动输入数量');
    }
    emit(state.copyWith(
      quantityInput: double.parse(barcode),
      errorMessage: null,
    ));
  }
  String _buildMessage(String prefix) {
    if (state.storeSite.isEmpty) {
      return '$prefix请扫描库位';
    }
    if (state.sn.isEmpty && state.batchNo.isEmpty) {
      return '$prefix请扫描条码';
    }
    if (state.quantityInput == null) {
      return '$prefix请录入数量';
    }
    return prefix;
  }
  Future<void> _checkMat(String matCode, String batchNo) async {
    for (final OutboundTaskItem item in state.taskItems) {
      if (item.matCode == matCode &&
          item.batchNo == batchNo &&
          item.storeSite == state.storeSite) {
        emit(state.copyWith(erpRoom: item.erpStore));
        return;
      }
    }
    for (final OutboundTaskItem item in state.taskItems) {
      if (item.matCode == matCode && item.storeSite == state.storeSite) {
        emit(state.copyWith(erpRoom: item.erpStore));
        return;
      }
    }
    throw Exception('明细中不存在物料$matCode');
  }
  Future<void> _checkMtlSite(
    String siteCode,
    String batch,
    String matCode,
    String matControl,
  ) async {
    if (matControl == '0') {
      return;
    }
    if (matCode.isEmpty) {
      return;
    }
    if (state.siteRequired && siteCode.isEmpty) {
      return;
    }
    if (state.batchRequired && (matControl == '1' || matControl == '2') &&
        batch.isEmpty) {
      return;
    }

    if (state.siteRequired) {
      for (final OutboundTaskItem item in state.taskItems) {
        final bool matchBatch = state.batchRequired && (matControl == '1' || matControl == '2')
            ? item.batchNo == batch
            : true;
        if (item.matCode == matCode && matchBatch && item.storeSite == siteCode) {
          emit(state.copyWith(erpRoom: item.erpStore));
          return;
        }
      }
      if (state.batchRequired) {
        throw Exception('物料$matCode批次$batch库位$siteCode不在明细中');
      } else {
        throw Exception('物料$matCode库位$siteCode不在明细中');
      }
    } else {
      for (final OutboundTaskItem item in state.taskItems) {
        final bool matchBatch = state.batchRequired && (matControl == '1' || matControl == '2')
            ? item.batchNo == batch
            : true;
        if (item.matCode == matCode && matchBatch) {
          emit(state.copyWith(erpRoom: item.erpStore));
          return;
        }
      }
      if (state.batchRequired) {
        throw Exception('物料$matCode批次$batch不在明细中');
      } else {
        throw Exception('物料$matCode不在明细中');
      }
    }
  }
  Future<String> _checkInventory(double collectQty) async {
    if (state.storeSite.isEmpty || state.matCode.isEmpty) {
      return '';
    }
    final List<OutboundInventoryRecord> inventory =
        await repository.fetchInventory(
      storeSite: state.storeSite,
      matCode: state.matCode,
      batchNo: state.batchNo,
      sn: state.sn,
      erpStore: state.erpRoom,
    );
    double repQty = 0;
    String key = '';
    String erpStoreInv = '';

    if (state.matControlFlag == '1' || state.matControlFlag == '2') {
      key = '${state.storeSite}${state.matCode}${state.batchNo}';
      final Iterable<OutboundInventoryRecord> matches = inventory.where((OutboundInventoryRecord record) {
        final bool matchBatch = record.batchNo == state.batchNo;
        final bool matchErp = state.erpRoom.isEmpty || record.erpStore == state.erpRoom;
        return record.matCode == state.matCode && matchBatch && matchErp;
      });
      if (matches.isEmpty) {
        throw Exception('物料${state.matCode}批次${state.batchNo}在库位${state.storeSite}不存在库存');
      }
      repQty = matches.fold<double>(0, (double sum, OutboundInventoryRecord record) => sum + record.quantity);
      erpStoreInv = matches.first.erpStore;
    } else {
      key = '${state.storeSite}${state.matCode}${state.sn}';
      final Iterable<OutboundInventoryRecord> matches = inventory.where((OutboundInventoryRecord record) {
        final bool matchSn = record.sn == state.sn;
        final bool matchErp = state.erpRoom.isEmpty || record.erpStore == state.erpRoom;
        final bool matchBatch = state.batchNo.isEmpty || record.batchNo == state.batchNo;
        return record.matCode == state.matCode && matchSn && matchErp && matchBatch;
      });
      if (matches.isEmpty) {
        throw Exception('物料${state.matCode}批次${state.batchNo}序列号${state.sn}在库位${state.storeSite}不存在库存');
      }
      repQty = matches.fold<double>(0, (double sum, OutboundInventoryRecord record) => sum + record.quantity);
      erpStoreInv = matches.first.erpStore;
    }

    if (state.erpRoom.isNotEmpty && erpStoreInv.isNotEmpty && state.erpRoom != erpStoreInv) {
      throw Exception('明细指定ERP子库${state.erpRoom}与库存${erpStoreInv}不一致');
    }

    final double reserved = state.reservedInventory[key] ?? 0;
    if (collectQty > 0 && repQty - reserved < collectQty) {
      throw Exception('库位${state.storeSite}物料${state.matCode}可用数量${repQty - reserved}不足以采集${collectQty}');
    }

    emit(state.copyWith(
      inventoryQty: repQty,
      erpStoreInv: erpStoreInv,
    ));
    return key;
  }
  Future<void> _dealQuantity(double collectQty, String matFlag) async {
    if (state.matControlFlag.isEmpty) {
      throw Exception('未获取到物料控制属性');
    }
    if (collectQty <= 0) {
      throw Exception('采集数量不能为0');
    }

    final String inventoryKey = await _checkInventory(collectQty);

    double totalTaskQty = 0;
    double totalCollectedQty = 0;
    for (final OutboundTaskItem item in state.taskItems) {
      if (item.matCode != state.matCode) {
        continue;
      }
      bool match = true;
      if ((matFlag == '1' || matFlag == '2') &&
          ((state.matSendControl == '0' && state.roomMatControl == '0') ||
              state.roomMatControl == '1')) {
        if (state.checkMode == OutboundMtlCheckMode.mtlBatch) {
          match = item.batchNo == state.batchNo;
        } else if (state.checkMode == OutboundMtlCheckMode.mtlBatchSite) {
          match = item.batchNo == state.batchNo && item.storeSite == state.storeSite;
        } else if (state.checkMode == OutboundMtlCheckMode.mtlSite) {
          match = item.storeSite == state.storeSite;
        }
      }
      if (!match) {
        continue;
      }
      totalTaskQty += item.hintQty;
      totalCollectedQty += item.collectedQty;
    }

    if (totalCollectedQty + collectQty > totalTaskQty) {
      throw Exception('采集数量$collectQty超过剩余可采集数量${totalTaskQty - totalCollectedQty}');
    }

    double remainingQty = collectQty;
    final List<OutboundTaskItem> updatedItems = List<OutboundTaskItem>.from(state.taskItems);
    final Map<String, OutboundCollectedSummary> summaries =
        Map<String, OutboundCollectedSummary>.from(state.collectedSummary);
    final Map<String, double> reservedInv =
        Map<String, double>.from(state.reservedInventory);
    final Map<String, String> dicSeq =
        Map<String, String>.from(state.scannedSerials);
    final Map<String, _OperationQuantities> operations = <String, _OperationQuantities>{};
    bool matchedDetail = false;
    double availableRepQty = state.inventoryQty ?? 0;

    for (int i = 0; i < updatedItems.length; i++) {
      if (remainingQty <= 0) {
        break;
      }
      final OutboundTaskItem item = updatedItems[i];
      if (item.matCode != state.matCode) {
        continue;
      }
      if ((state.matSendControl == '0' && state.roomMatControl == '0') ||
          state.roomMatControl == '1') {
        if (matFlag == '1' || matFlag == '2') {
          if (state.checkMode == OutboundMtlCheckMode.mtlBatch &&
              item.batchNo != state.batchNo) {
            continue;
          }
          if (state.checkMode == OutboundMtlCheckMode.mtlBatchSite &&
              (item.batchNo != state.batchNo || item.storeSite != state.storeSite)) {
            continue;
          }
          if (state.checkMode == OutboundMtlCheckMode.mtlSite &&
              item.storeSite != state.storeSite) {
            continue;
          }
        }
      }

      if (item.hintQty == item.collectedQty) {
        continue;
      }

      matchedDetail = true;
      final double available = item.hintQty - item.collectedQty;
      final double consume = remainingQty >= available ? available : remainingQty;
      remainingQty -= consume;

      availableRepQty -= consume;
      final double previousCollected = item.collectedQty;
      final double newCollected = item.collectedQty + consume;
      double newRep = item.repertoryQty - consume;
      if (state.matControlFlag == '0') {
        newRep = 0;
      }

      updatedItems[i] = item.copyWith(
        collectedQty: newCollected,
        repertoryQty: newRep,
      );

      final OutboundCollectedSummary summary = summaries[item.outTaskItemId] ??
          OutboundCollectedSummary(
            originalCollectedQty: previousCollected,
            currentCollectedQty: previousCollected,
            matCode: item.matCode,
          );
      summaries[item.outTaskItemId] = summary.copyWith(
        currentCollectedQty: newCollected,
      );

      operations[item.outTaskItemId] = _OperationQuantities(
        taskQty: item.hintQty,
        collectedQty: consume,
      );
    }

    if ((state.matSendControl == '0' && state.roomMatControl == '0') ||
        state.roomMatControl == '1') {
      if (!matchedDetail) {
        throw Exception('采集信息未与明细匹配');
      }
    }

    if (state.sn.isNotEmpty) {
      dicSeq['${state.matCode}@${state.sn}'] = '${state.matCode}@${state.sn}';
    }

    final double currentReserved = reservedInv[inventoryKey] ?? 0;
    reservedInv[inventoryKey] = currentReserved + collectQty;

    final List<OutboundCollectionRecord> newRecords =
        List<OutboundCollectionRecord>.from(state.collectionRecords);
    for (final MapEntry<String, _OperationQuantities> entry in operations.entries) {
      final OutboundTaskItem item = updatedItems.firstWhere(
        (OutboundTaskItem element) => element.outTaskItemId == entry.key,
      );
      final OutboundCollectionRecord record = OutboundCollectionRecord(
        matCode: state.matCode,
        matName: state.matName,
        batchNo: state.batchNo,
        sn: state.sn,
        collectQty: entry.value.collectedQty,
        taskQty: entry.value.taskQty,
        storeRoom: item.storeRoom,
        storeSite: state.storeSite,
        outTaskItemId: entry.key,
        erpStore: state.erpStoreInv,
        trayNo: item.trayNo,
      );
      newRecords.add(record);
      final Box<OutboundCollectionRecord> box =
          _localBox ??= await _localBoxFuture;
      await box.add(record);
    }

    emit(state.copyWith(
      taskItems: updatedItems,
      filteredItems: _queryTask(state.storeSite, state.matCode, items: updatedItems),
      collectedSummary: summaries,
      reservedInventory: reservedInv,
      collectionRecords: newRecords,
      scannedSerials: dicSeq,
      inventoryQty: availableRepQty,
    ));
  }
  void _initializeCollect() {
    emit(state.copyWith(
      matCode: '',
      matName: '',
      matInnerCode: '',
      batchNo: '',
      sn: '',
      quantityInput: null,
      inventoryQty: 0,
      erpStoreInv: '',
      messageLabel: '请扫描库位',
    ));
  }
  List<OutboundTaskItem> _queryTask(String barcode, String matCode,
      {List<OutboundTaskItem>? items}) {
    final Iterable<OutboundTaskItem> source = (items ?? state.taskItems);
    return source.where((OutboundTaskItem item) {
      final bool matchSite = barcode.isEmpty || item.storeSite == barcode;
      final bool matchMat = matCode.isEmpty || item.matCode == matCode;
      return matchSite && matchMat;
    }).toList();
  }
  Future<void> _submitCollection() async {
    if (state.collectionRecords.isEmpty) {
      emit(state.copyWith(errorMessage: '没有采集明细，请先采集'));
      return;
    }

    String msg = '';
    for (final OutboundTaskItem item in state.taskItems) {
      if (item.hintQty != item.collectedQty) {
        msg = '库位${item.storeSite}物料${item.matCode}剩余${item.hintQty - item.collectedQty}未采集,是否提交?';
        break;
      }
    }

    if (msg.isNotEmpty) {
      emit(state.copyWith(
        pendingAction: OutboundPendingAction.commit,
        pendingMessage: msg,
      ));
      return;
    }

    await _doCommit();
  }

  Future<void> _confirmPendingAction(bool confirmed) async {
    if (!confirmed) {
      emit(state.copyWith(
        pendingAction: OutboundPendingAction.none,
        pendingMessage: null,
      ));
      return;
    }

    switch (state.pendingAction) {
      case OutboundPendingAction.commit:
        await _doCommit();
        break;
      case OutboundPendingAction.close:
        emit(state.copyWith(
          shouldClose: true,
          pendingAction: OutboundPendingAction.none,
          pendingMessage: null,
        ));
        break;
      case OutboundPendingAction.finish:
        await _doFinishSelected();
        break;
      case OutboundPendingAction.none:
        break;
    }
  }

  Future<void> _doCommit() async {
    emit(state.copyWith(isProcessing: true));
    try {
      final List<DownShelvesInfoPayload> downShelves = state.collectionRecords
          .map(
            (OutboundCollectionRecord record) => DownShelvesInfoPayload(
              taskNo: taskNo,
              matCode: record.matCode,
              batchNo: record.batchNo,
              sn: record.sn,
              collectQty: record.collectQty,
              storeRoomNo: record.storeRoom,
              storeSiteNo: record.storeSite,
              outTaskItemId: record.outTaskItemId,
              erpStore: record.erpStore,
              trayNo: record.trayNo,
            ),
          )
          .toList();
      final List<ItemListInfoPayload> itemList = state.collectedSummary.entries
          .map(
            (MapEntry<String, OutboundCollectedSummary> entry) =>
                ItemListInfoPayload(
                  outTaskItemId: entry.key,
                  originalQty: entry.value.originalCollectedQty,
                  collectedQty: entry.value.currentCollectedQty,
                  matCode: entry.value.matCode,
                ),
          )
          .toList();

      for (final OutboundCollectionRecord record in state.collectionRecords) {
        await repository.validateStoreSite(
          storeRoom: record.storeRoom,
          storeSite: record.storeSite,
        );
      }

      await repository.commitDownShelves(
        downShelves: downShelves,
        items: itemList,
        userId: userId,
      );

      final Box<OutboundCollectionRecord> box =
          _localBox ??= await _localBoxFuture;
      await box.clear();
      await _initialize();
      emit(state.copyWith(
        isProcessing: false,
        collectionRecords: <OutboundCollectionRecord>[],
        collectedSummary: <String, OutboundCollectedSummary>{},
        reservedInventory: <String, double>{},
        scannedSerials: <String, String>{},
        successMessage: '提交成功',
        pendingAction: OutboundPendingAction.none,
        pendingMessage: null,
      ));
    } catch (error) {
      emit(state.copyWith(
        isProcessing: false,
        errorMessage: error.toString(),
        pendingAction: OutboundPendingAction.none,
        pendingMessage: null,
      ));
    }
  }

  void _requestClose() {
    emit(state.copyWith(
      pendingAction: OutboundPendingAction.close,
      pendingMessage: '当前采集${state.collectionRecords.length}条,确认关闭?',
    ));
  }

  void _toggleSelection(String outTaskItemId) {
    final Set<String> selected = Set<String>.from(state.selectedItemIds);
    if (selected.contains(outTaskItemId)) {
      selected.remove(outTaskItemId);
    } else {
      selected.add(outTaskItemId);
    }
    emit(state.copyWith(selectedItemIds: selected));
  }

  Future<void> _finishSelected() async {
    if (state.collectionRecords.isNotEmpty) {
      emit(state.copyWith(errorMessage: '存在未提交的采集记录，无法标记完成'));
      return;
    }
    if (state.selectedItemIds.isEmpty) {
      emit(state.copyWith(errorMessage: '请先选择需要完成的明细'));
      return;
    }
    emit(state.copyWith(
      pendingAction: OutboundPendingAction.finish,
      pendingMessage: '确认将选中明细标记为完成?',
    ));
  }

  Future<void> _doFinishSelected() async {
    emit(state.copyWith(isProcessing: true));
    try {
      for (final String id in state.selectedItemIds) {
        await repository.commitFinishOutTaskItem(
          outTaskItemId: id,
          userId: userId,
          roomTag: roomTag,
        );
      }
      await _initialize();
      emit(state.copyWith(
        isProcessing: false,
        selectedItemIds: <String>{},
        successMessage: '完成成功',
        pendingAction: OutboundPendingAction.none,
        pendingMessage: null,
      ));
    } catch (error) {
      emit(state.copyWith(
        isProcessing: false,
        errorMessage: error.toString(),
        pendingAction: OutboundPendingAction.none,
        pendingMessage: null,
      ));
    }
  }

  void _clearSuccessMessage() {
    emit(state.copyWith(successMessage: null));
  }

  void _clearErrorMessage() {
    if (state.errorMessage != null) {
      emit(state.copyWith(errorMessage: null));
    }
  }

  void _openCollectionDetail() {
    emit(state.copyWith(
      navigationTarget: OutboundNavigation.detail,
      navigationArguments: <String, dynamic>{
        'records': state.collectionRecords,
      },
    ));
  }

  void _openExceptionTask() {
    if (state.collectionRecords.isNotEmpty) {
      emit(state.copyWith(errorMessage: '采集未提交,请先提交后再登记异常'));
      return;
    }
    emit(state.copyWith(
      navigationTarget: OutboundNavigation.exception,
      navigationArguments: <String, dynamic>{
        'taskComment': taskComment,
        'taskNo': taskNo,
        'taskId': taskId,
        'storeRoom': storeRoom,
      },
    ));
  }

  void _openSupplierTask() {
    emit(state.copyWith(
      navigationTarget: OutboundNavigation.supplier,
      navigationArguments: <String, dynamic>{
        'taskComment': taskComment,
        'taskNo': taskNo,
        'taskId': taskId,
        'storeRoom': storeRoom,
      },
    ));
  }

  void _clearNavigation() {
    emit(state.copyWith(navigationTarget: null, navigationArguments: null));
  }

  void _acknowledgeCloseHandled() {
    if (state.shouldClose) {
      emit(state.copyWith(shouldClose: false));
    }
  }
