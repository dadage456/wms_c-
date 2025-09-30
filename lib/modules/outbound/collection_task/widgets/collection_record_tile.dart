import 'package:flutter/material.dart';

import '../models/outbound_collection_record.dart';

class CollectionRecordTile extends StatelessWidget {
  const CollectionRecordTile({super.key, required this.record});

  final OutboundCollectionRecord record;

  @override
  Widget build(BuildContext context) {
    return ListTile(
      dense: true,
      title: Text('${record.matCode} 批次:${record.batchNo} 序列:${record.sn}'),
      subtitle: Text('数量:${record.collectQty} 库位:${record.storeSite} ERP:${record.erpStore}'),
    );
  }
}
