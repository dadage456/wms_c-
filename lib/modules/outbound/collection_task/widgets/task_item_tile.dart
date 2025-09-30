import 'package:flutter/material.dart';

import '../models/outbound_task_item.dart';

class TaskItemTile extends StatelessWidget {
  const TaskItemTile({
    super.key,
    required this.item,
    required this.selected,
    required this.onSelected,
  });

  final OutboundTaskItem item;
  final bool selected;
  final ValueChanged<bool?> onSelected;

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.symmetric(vertical: 4),
      child: CheckboxListTile(
        value: selected,
        onChanged: onSelected,
        title: Text('${item.matCode} (${item.matName})'),
        subtitle: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Text('库位: ${item.storeSite}  库房: ${item.storeRoom}'),
            Text('任务数量: ${item.hintQty}  已采: ${item.collectedQty}'),
            if (item.batchNo.isNotEmpty) Text('批次: ${item.batchNo}'),
            if (item.sn.isNotEmpty) Text('序列号: ${item.sn}'),
            if (item.erpStore.isNotEmpty) Text('ERP子库: ${item.erpStore}'),
          ],
        ),
      ),
    );
  }
}
