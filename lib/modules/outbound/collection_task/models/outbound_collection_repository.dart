import 'dart:async';

import 'package:dio/dio.dart';
import 'package:hive/hive.dart';

import 'barcode_content.dart';
import 'mat_control_info.dart';
import 'outbound_collection_record.dart';
import 'outbound_down_shelves_payload.dart';
import 'outbound_inventory_record.dart';
import 'outbound_task_item.dart';

class OutboundCollectionRepository {
  OutboundCollectionRepository(this._dio);

  final Dio _dio;

  Future<List<OutboundTaskItem>> fetchTaskItems({
    required String taskNo,
    required String userId,
    required String roleId,
    required String workStation,
    required String roomTag,
    required String taskComment,
  }) async {
    final response = await _dio.get<Map<String, dynamic>>(
      '/system/terminal/outTaskitemList',
      queryParameters: <String, dynamic>{
        'outtaskid': taskNo,
        'userId': userId,
        'roleoRuserId': roleId,
        'workstation': workStation,
        'roomTag': roomTag,
        'searchKey': taskComment,
        'PageIndex': '1',
        'PageSize': '500',
        'batchflag': '0',
        'transferType': '0',
        'beatflag': 'N',
      },
    );
    final List<dynamic> rows =
        response.data?['data']?['rows'] as List<dynamic>? ?? <dynamic>[];
    return rows
        .map((dynamic e) => OutboundTaskItem.fromJson(e as Map<String, dynamic>))
        .toList();
  }

  Future<String> getRoomMatControl(String taskId) async {
    final response = await _dio.get<Map<String, dynamic>>(
      '/system/terminal/getRoomMatControl',
      queryParameters: <String, dynamic>{'taskId': taskId},
    );
    final data = response.data?['data'];
    if (data is Map<String, dynamic>) {
      return (data['matcontrol'] ?? data['roomMatControl'] ?? '0').toString();
    }
    final raw = response.data?['msg']?.toString() ?? '0';
    return raw.split('!').length > 4 ? raw.split('!')[4] : '0';
  }

  Future<MatControlInfo> getMatControl(String matCode) async {
    final response = await _dio.get<Map<String, dynamic>>(
      '/system/terminal/getMatControl',
      queryParameters: <String, dynamic>{'matCode': matCode},
    );
    final data = response.data?['data'];
    if (data is Map<String, dynamic>) {
      return MatControlInfo.fromJson(data);
    }
    final raw = response.data?['msg']?.toString() ?? '';
    return MatControlInfo.fromRaw(raw);
  }

  Future<BarcodeContent> analysisBarcode(String barcode) async {
    final response = await _dio.get<Map<String, dynamic>>(
      '/system/terminal/analysisBarcode',
      queryParameters: <String, dynamic>{'barcode': barcode},
    );
    final data = response.data?['data'];
    if (data is Map<String, dynamic>) {
      return BarcodeContent.fromJson(data);
    }
    throw DioException(
      requestOptions: response.requestOptions,
      error: '条码解析失败',
    );
  }

  Future<void> validateStoreSite({
    required String storeRoom,
    required String storeSite,
  }) async {
    final response = await _dio.get<Map<String, dynamic>>(
      '/system/terminal/getStoreSite',
      queryParameters: <String, dynamic>{
        'storeRoomNo': storeRoom,
        'storeSiteNo': storeSite,
      },
    );
    if (response.data == null || response.data?['code'] != 200) {
      throw DioException(
        requestOptions: response.requestOptions,
        error: response.data?['msg'] ?? '库位校验失败',
      );
    }
    final status = response.data?['data']?['status']?.toString();
    if (status != null && status != '0') {
      throw DioException(
        requestOptions: response.requestOptions,
        error: '库位$storeSite已冻结',
      );
    }
  }

  Future<List<OutboundInventoryRecord>> fetchInventory({
    required String storeSite,
    required String matCode,
    String batchNo = '',
    String sn = '',
    String erpStore = '',
  }) async {
    final response = await _dio.get<Map<String, dynamic>>(
      '/system/terminal/getRepertoryByStoresiteNo',
      queryParameters: <String, dynamic>{
        'storesiteno': storeSite,
        'matcode': matCode,
        'batchno': batchNo,
        'sn': sn,
        'erpStoreroom': erpStore,
      },
    );
    final List<dynamic> rows = response.data?['data'] is List
        ? response.data?['data'] as List<dynamic>
        : response.data?['data']?['rows'] as List<dynamic>? ?? <dynamic>[];
    return rows
        .map((dynamic e) =>
            OutboundInventoryRecord.fromJson(e as Map<String, dynamic>))
        .toList();
  }

  Future<void> commitDownShelves({
    required List<DownShelvesInfoPayload> downShelves,
    required List<ItemListInfoPayload> items,
    required String userId,
  }) async {
    await _dio.post<void>(
      '/system/terminal/commitDownShelves',
      data: <String, dynamic>{
        'downShelvesInfos': downShelves.map((e) => e.toJson()).toList(),
        'itemListInfos': items.map((e) => e.toJson()).toList(),
        'userId': userId,
      },
    );
  }

  Future<void> commitFinishOutTaskItem({
    required String outTaskItemId,
    required String userId,
    required String roomTag,
  }) async {
    await _dio.post<void>(
      '/system/terminal/commitFinishOutTaskItem',
      data: <String, dynamic>{
        'outtaskitemid': outTaskItemId,
        'userId': userId,
        'roomTag': roomTag,
        'isCanel': false,
      },
    );
  }

  Future<List<OutboundCollectionRecord>> loadLocalCollections(
      Box<OutboundCollectionRecord> box) async {
    return box.values.toList();
  }
}
