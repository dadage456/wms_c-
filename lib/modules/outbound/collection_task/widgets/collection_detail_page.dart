import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../bloc/outbound_collection_bloc.dart';
import '../models/outbound_collection_record.dart';

class CollectionDetailPage extends StatefulWidget {
  const CollectionDetailPage({super.key});

  @override
  State<CollectionDetailPage> createState() => _CollectionDetailPageState();
}

class _CollectionDetailPageState extends State<CollectionDetailPage> {
  late int _lastLength;

  @override
  void initState() {
    super.initState();
    _lastLength =
        context.read<OutboundCollectionBloc>().state.collectionRecords.length;
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<OutboundCollectionBloc, OutboundCollectionState>(
      listenWhen: (OutboundCollectionState previous,
              OutboundCollectionState current) =>
          previous.collectionRecords.length !=
              current.collectionRecords.length ||
          previous.errorMessage != current.errorMessage,
      listener: (BuildContext context, OutboundCollectionState state) {
        final ScaffoldMessengerState messenger = ScaffoldMessenger.of(context);
        if (state.errorMessage != null && state.errorMessage!.isNotEmpty) {
          messenger.showSnackBar(
            SnackBar(content: Text(state.errorMessage!)),
          );
          context
              .read<OutboundCollectionBloc>()
              .add(const OutboundCollectionErrorCleared());
        } else if (state.collectionRecords.length < _lastLength) {
          messenger.showSnackBar(
            const SnackBar(content: Text('删除成功')),
          );
        }
        _lastLength = state.collectionRecords.length;
        if (state.collectionRecords.isEmpty && Navigator.of(context).canPop()) {
          Navigator.of(context).pop();
        }
      },
      child: Scaffold(
        appBar: AppBar(title: const Text('采集明细列表')),
        body: BlocBuilder<OutboundCollectionBloc, OutboundCollectionState>(
          builder: (BuildContext context, OutboundCollectionState state) {
            final List<OutboundCollectionRecord> records =
                state.collectionRecords;
            if (records.isEmpty) {
              return const Center(child: Text('暂无采集记录'));
            }
            return ListView.separated(
              itemCount: records.length,
              separatorBuilder: (_, __) => const Divider(height: 1),
              itemBuilder: (BuildContext context, int index) {
                final OutboundCollectionRecord record = records[index];
                final String snLabel =
                    record.sn.isEmpty ? '无' : record.sn;
                return ListTile(
                  title: Text(
                    '${record.matCode} 批次:${record.batchNo.isEmpty ? '无' : record.batchNo} 序列:$snLabel',
                  ),
                  subtitle: Text(
                    '数量:${record.collectQty} 任务量:${record.taskQty} 库位:${record.storeSite} ERP:${record.erpStore}',
                  ),
                  trailing: IconButton(
                    icon: const Icon(Icons.delete_forever),
                    onPressed: () => _handleDelete(record),
                  ),
                );
              },
            );
          },
        ),
      ),
    );
  }

  Future<void> _handleDelete(OutboundCollectionRecord record) async {
    final bool? confirmed = await showDialog<bool>(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('确认删除'),
          content: Text(
            '确定删除物料${record.matCode}库位${record.storeSite}的采集记录?\n数量:${record.collectQty}',
          ),
          actions: <Widget>[
            TextButton(
              onPressed: () => Navigator.of(context).pop(false),
              child: const Text('取消'),
            ),
            ElevatedButton(
              onPressed: () => Navigator.of(context).pop(true),
              child: const Text('确定'),
            ),
          ],
        );
      },
    );
    if (confirmed == true && mounted) {
      context
          .read<OutboundCollectionBloc>()
          .add(OutboundCollectionRecordDeleted(record));
    }
  }
}
