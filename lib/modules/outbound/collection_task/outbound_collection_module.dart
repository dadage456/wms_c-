import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:hive_flutter/hive_flutter.dart';

import 'models/outbound_collection_record.dart';
import 'models/outbound_collection_repository.dart';
import 'outbound_collection_page.dart';

class OutboundCollectionModule extends Module {
  @override
  List<Bind<Object>> get binds => <Bind<Object>>[
        Bind.singleton<Dio>(
          (i) => Dio(
            BaseOptions(
              baseUrl: 'http://10.12.8.123:8086',
              connectTimeout: const Duration(seconds: 30),
              receiveTimeout: const Duration(seconds: 30),
            ),
          ),
        ),
        Bind.singleton<OutboundCollectionRepository>(
          (i) => OutboundCollectionRepository(i<Dio>()),
        ),
        AsyncBind<Box<OutboundCollectionRecord>>((i) async {
          try {
            await Hive.initFlutter();
          } catch (_) {
            // already initialized
          }
          if (!Hive.isAdapterRegistered(OutboundCollectionRecordAdapter().typeId)) {
            Hive.registerAdapter(OutboundCollectionRecordAdapter());
          }
          if (Hive.isBoxOpen('outbound_collection')) {
            return Hive.box<OutboundCollectionRecord>('outbound_collection');
          }
          return Hive.openBox<OutboundCollectionRecord>('outbound_collection');
        }),
      ];

  @override
  List<ModularRoute> get routes => <ModularRoute>[
        ChildRoute(
          Modular.initialRoute,
          child: (_, args) {
            final Map<String, dynamic> data = args.data as Map<String, dynamic>? ?? <String, dynamic>{};
            return OutboundCollectionPage(
              taskNo: data['taskNo']?.toString() ?? '',
              taskId: data['taskId']?.toString() ?? '',
              storeRoom: data['storeRoom']?.toString() ?? '',
              siteFlag: data['siteFlag']?.toString() ?? 'Y',
              batchFlag: data['batchFlag']?.toString() ?? 'Y',
              taskComment: data['taskComment']?.toString() ?? '',
              workStation: data['workStation']?.toString() ?? '',
              userId: data['userId']?.toString() ?? '',
              roleId: data['roleId']?.toString() ?? '',
              roomTag: data['roomTag']?.toString() ?? '0',
            );
          },
        ),
        ChildRoute(
          '/detail',
          child: (_, args) {
            final Map<String, dynamic> data = args.data as Map<String, dynamic>? ?? <String, dynamic>{};
            final List<OutboundCollectionRecord> records =
                (data['records'] as List<OutboundCollectionRecord>? ?? <OutboundCollectionRecord>[]);
            return Scaffold(
              appBar: AppBar(title: const Text('采集明细列表')),
              body: ListView.builder(
                itemCount: records.length,
                itemBuilder: (_, int index) => ListTile(
                  title: Text('${records[index].matCode} 批次:${records[index].batchNo}'),
                  subtitle: Text('数量:${records[index].collectQty} 库位:${records[index].storeSite}'),
                ),
              ),
            );
          },
        ),
        ChildRoute(
          '/exception',
          child: (_, args) {
            final Map<String, dynamic> data = args.data as Map<String, dynamic>? ?? <String, dynamic>{};
            return Scaffold(
              appBar: AppBar(title: const Text('异常登记')),
              body: Padding(
                padding: const EdgeInsets.all(16),
                child: Text('任务:${data['taskNo'] ?? ''} 库房:${data['storeRoom'] ?? ''}'),
              ),
            );
          },
        ),
        ChildRoute(
          '/supplier',
          child: (_, args) {
            final Map<String, dynamic> data = args.data as Map<String, dynamic>? ?? <String, dynamic>{};
            return Scaffold(
              appBar: AppBar(title: const Text('供应商任务')),
              body: Padding(
                padding: const EdgeInsets.all(16),
                child: Text('任务:${data['taskNo'] ?? ''} 库房:${data['storeRoom'] ?? ''}'),
              ),
            );
          },
        ),
      ];
}
