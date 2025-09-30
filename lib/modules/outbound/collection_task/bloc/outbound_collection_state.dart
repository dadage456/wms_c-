part of 'outbound_collection_bloc.dart';

const Object _unset = Object();

enum OutboundCollectionStatus { initial, loading, ready, failure }

enum OutboundPendingAction { none, commit, close, finish }

enum OutboundMtlCheckMode { mtl, mtlBatch, mtlSite, mtlBatchSite }

enum OutboundNavigation { exception, supplier, detail }

class OutboundCollectionState extends Equatable {
  const OutboundCollectionState({
    required this.status,
    required this.taskItems,
    required this.filteredItems,
    required this.collectionRecords,
    required this.collectedSummary,
    required this.reservedInventory,
    required this.scannedSerials,
    required this.messageLabel,
    required this.siteRequired,
    required this.batchRequired,
    required this.checkMode,
    required this.storeRoom,
    required this.storeSite,
    required this.matCode,
    required this.matName,
    required this.matInnerCode,
    required this.batchNo,
    required this.sn,
    required this.quantityInput,
    required this.matControlFlag,
    required this.matSendControl,
    required this.erpRoom,
    required this.erpStoreInv,
    required this.inventoryQty,
    required this.roomMatControl,
    required this.errorMessage,
    required this.successMessage,
    required this.pendingAction,
    required this.pendingMessage,
    required this.selectedItemIds,
    required this.shouldClose,
    required this.isProcessing,
    required this.navigationTarget,
    required this.navigationArguments,
  });

  factory OutboundCollectionState.initial({
    required String storeRoom,
    required String siteFlag,
    required String batchFlag,
  }) {
    final bool siteRequired = siteFlag == 'Y';
    final bool batchRequired = batchFlag == 'Y';
    OutboundMtlCheckMode mode = OutboundMtlCheckMode.mtl;
    if (siteRequired && batchRequired) {
      mode = OutboundMtlCheckMode.mtlBatchSite;
    } else if (siteRequired) {
      mode = OutboundMtlCheckMode.mtlSite;
    } else if (batchRequired) {
      mode = OutboundMtlCheckMode.mtlBatch;
    }

    return OutboundCollectionState(
      status: OutboundCollectionStatus.initial,
      taskItems: const <OutboundTaskItem>[],
      filteredItems: const <OutboundTaskItem>[],
      collectionRecords: const <OutboundCollectionRecord>[],
      collectedSummary: const <String, OutboundCollectedSummary>{},
      reservedInventory: const <String, double>{},
      scannedSerials: const <String, String>{},
      messageLabel: '请扫描库位',
      siteRequired: siteRequired,
      batchRequired: batchRequired,
      checkMode: mode,
      storeRoom: storeRoom,
      storeSite: '',
      matCode: '',
      matName: '',
      matInnerCode: '',
      batchNo: '',
      sn: '',
      quantityInput: null,
      matControlFlag: '',
      matSendControl: '0',
      erpRoom: '',
      erpStoreInv: '',
      inventoryQty: 0,
      roomMatControl: '0',
      errorMessage: null,
      successMessage: null,
      pendingAction: OutboundPendingAction.none,
      pendingMessage: null,
      selectedItemIds: const <String>{},
      shouldClose: false,
      isProcessing: false,
      navigationTarget: null,
      navigationArguments: null,
    );
  }

  OutboundCollectionState copyWith({
    OutboundCollectionStatus? status,
    List<OutboundTaskItem>? taskItems,
    List<OutboundTaskItem>? filteredItems,
    List<OutboundCollectionRecord>? collectionRecords,
    Map<String, OutboundCollectedSummary>? collectedSummary,
    Map<String, double>? reservedInventory,
    Map<String, String>? scannedSerials,
    String? messageLabel,
    bool? siteRequired,
    bool? batchRequired,
    OutboundMtlCheckMode? checkMode,
    String? storeRoom,
    String? storeSite,
    String? matCode,
    String? matName,
    String? matInnerCode,
    String? batchNo,
    String? sn,
    double? quantityInput,
    String? matControlFlag,
    String? matSendControl,
    String? erpRoom,
    String? erpStoreInv,
    double? inventoryQty,
    String? roomMatControl,
    Object? errorMessage = _unset,
    Object? successMessage = _unset,
    OutboundPendingAction? pendingAction,
    Object? pendingMessage = _unset,
    Set<String>? selectedItemIds,
    bool? shouldClose,
    bool? isProcessing,
    Object? navigationTarget = _unset,
    Object? navigationArguments = _unset,
  }) {
    return OutboundCollectionState(
      status: status ?? this.status,
      taskItems: taskItems ?? this.taskItems,
      filteredItems: filteredItems ?? this.filteredItems,
      collectionRecords: collectionRecords ?? this.collectionRecords,
      collectedSummary: collectedSummary ?? this.collectedSummary,
      reservedInventory: reservedInventory ?? this.reservedInventory,
      scannedSerials: scannedSerials ?? this.scannedSerials,
      messageLabel: messageLabel ?? this.messageLabel,
      siteRequired: siteRequired ?? this.siteRequired,
      batchRequired: batchRequired ?? this.batchRequired,
      checkMode: checkMode ?? this.checkMode,
      storeRoom: storeRoom ?? this.storeRoom,
      storeSite: storeSite ?? this.storeSite,
      matCode: matCode ?? this.matCode,
      matName: matName ?? this.matName,
      matInnerCode: matInnerCode ?? this.matInnerCode,
      batchNo: batchNo ?? this.batchNo,
      sn: sn ?? this.sn,
      quantityInput: quantityInput ?? this.quantityInput,
      matControlFlag: matControlFlag ?? this.matControlFlag,
      matSendControl: matSendControl ?? this.matSendControl,
      erpRoom: erpRoom ?? this.erpRoom,
      erpStoreInv: erpStoreInv ?? this.erpStoreInv,
      inventoryQty: inventoryQty ?? this.inventoryQty,
      roomMatControl: roomMatControl ?? this.roomMatControl,
      errorMessage: identical(errorMessage, _unset)
          ? this.errorMessage
          : errorMessage as String?,
      successMessage: identical(successMessage, _unset)
          ? this.successMessage
          : successMessage as String?,
      pendingAction: pendingAction ?? this.pendingAction,
      pendingMessage: identical(pendingMessage, _unset)
          ? this.pendingMessage
          : pendingMessage as String?,
      selectedItemIds: selectedItemIds ?? this.selectedItemIds,
      shouldClose: shouldClose ?? this.shouldClose,
      isProcessing: isProcessing ?? this.isProcessing,
      navigationTarget: identical(navigationTarget, _unset)
          ? this.navigationTarget
          : navigationTarget as OutboundNavigation?,
      navigationArguments: identical(navigationArguments, _unset)
          ? this.navigationArguments
          : navigationArguments as Map<String, dynamic>?,
    );
  }

  final OutboundCollectionStatus status;
  final List<OutboundTaskItem> taskItems;
  final List<OutboundTaskItem> filteredItems;
  final List<OutboundCollectionRecord> collectionRecords;
  final Map<String, OutboundCollectedSummary> collectedSummary;
  final Map<String, double> reservedInventory;
  final Map<String, String> scannedSerials;
  final String messageLabel;
  final bool siteRequired;
  final bool batchRequired;
  final OutboundMtlCheckMode checkMode;
  final String storeRoom;
  final String storeSite;
  final String matCode;
  final String matName;
  final String matInnerCode;
  final String batchNo;
  final String sn;
  final double? quantityInput;
  final String matControlFlag;
  final String matSendControl;
  final String erpRoom;
  final String erpStoreInv;
  final double? inventoryQty;
  final String roomMatControl;
  final String? errorMessage;
  final String? successMessage;
  final OutboundPendingAction pendingAction;
  final String? pendingMessage;
  final Set<String> selectedItemIds;
  final bool shouldClose;
  final bool isProcessing;
  final OutboundNavigation? navigationTarget;
  final Map<String, dynamic>? navigationArguments;

  @override
  List<Object?> get props => <Object?>[
        status,
        taskItems,
        filteredItems,
        collectionRecords,
        collectedSummary,
        reservedInventory,
        scannedSerials,
        messageLabel,
        siteRequired,
        batchRequired,
        checkMode,
        storeRoom,
        storeSite,
        matCode,
        matName,
        matInnerCode,
        batchNo,
        sn,
        quantityInput,
        matControlFlag,
        matSendControl,
        erpRoom,
        erpStoreInv,
        inventoryQty,
        roomMatControl,
        errorMessage,
        successMessage,
        pendingAction,
        pendingMessage,
        selectedItemIds,
        shouldClose,
        isProcessing,
        navigationTarget,
        navigationArguments,
      ];
}
