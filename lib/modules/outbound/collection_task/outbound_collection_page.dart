import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:hive/hive.dart';

import 'bloc/outbound_collection_bloc.dart';
import 'models/outbound_collection_record.dart';
import 'models/outbound_collection_repository.dart';
import 'models/outbound_task_item.dart';
import 'widgets/collection_record_tile.dart';
import 'widgets/task_item_tile.dart';

class OutboundCollectionPage extends StatelessWidget {
  const OutboundCollectionPage({
    super.key,
    required this.taskNo,
    required this.taskId,
    required this.storeRoom,
    required this.siteFlag,
    required this.batchFlag,
    required this.taskComment,
    required this.workStation,
    required this.userId,
    required this.roleId,
    required this.roomTag,
  });

  final String taskNo;
  final String taskId;
  final String storeRoom;
  final String siteFlag;
  final String batchFlag;
  final String taskComment;
  final String workStation;
  final String userId;
  final String roleId;
  final String roomTag;

  @override
  Widget build(BuildContext context) {
    final OutboundCollectionRepository repository =
        Modular.get<OutboundCollectionRepository>();
    final Future<Box<OutboundCollectionRecord>> localBox =
        Modular.getAsync<Box<OutboundCollectionRecord>>();

    return BlocProvider<OutboundCollectionBloc>(
      create: (_) => OutboundCollectionBloc(
        repository: repository,
        taskNo: taskNo,
        taskId: taskId,
        taskComment: taskComment,
        workStation: workStation,
        storeRoom: storeRoom,
        siteFlag: siteFlag,
        batchFlag: batchFlag,
        userId: userId,
        roleId: roleId,
        roomTag: roomTag,
        localBox: localBox,
      ),
      child: const _OutboundCollectionView(),
    );
  }
}

class _OutboundCollectionView extends StatefulWidget {
  const _OutboundCollectionView();

  @override
  State<_OutboundCollectionView> createState() => _OutboundCollectionViewState();
}

class _OutboundCollectionViewState extends State<_OutboundCollectionView> {
  late final TextEditingController _barcodeController;
  late final FocusNode _barcodeFocus;

  @override
  void initState() {
    super.initState();
    _barcodeController = TextEditingController();
    _barcodeFocus = FocusNode();
  }

  @override
  void dispose() {
    _barcodeController.dispose();
    _barcodeFocus.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<OutboundCollectionBloc, OutboundCollectionState>(
      listenWhen: (OutboundCollectionState previous, OutboundCollectionState current) =>
          previous.errorMessage != current.errorMessage ||
          previous.successMessage != current.successMessage ||
          previous.pendingAction != current.pendingAction ||
          previous.shouldClose != current.shouldClose ||
          previous.navigationTarget != current.navigationTarget,
      listener: (BuildContext context, OutboundCollectionState state) async {
        final OutboundCollectionBloc bloc =
            context.read<OutboundCollectionBloc>();
        final messenger = ScaffoldMessenger.of(context);
        if (state.errorMessage != null) {
          messenger.showSnackBar(
            SnackBar(content: Text(state.errorMessage!)),
          );
          bloc.add(const OutboundCollectionErrorCleared());
        }
        if (state.successMessage != null) {
          messenger.showSnackBar(
            SnackBar(content: Text(state.successMessage!)),
          );
          bloc.add(const OutboundCollectionSuccessCleared());
        }
        if (state.pendingAction != OutboundPendingAction.none &&
            state.pendingMessage != null) {
          final bool? confirmed = await showDialog<bool>(
            context: context,
            builder: (BuildContext context) {
              return AlertDialog(
                title: const Text('确认操作'),
                content: Text(state.pendingMessage!),
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
          if (!mounted) return;
          bloc.add(
            OutboundCollectionPendingActionConfirmed(confirmed ?? false),
          );

        }
        if (state.shouldClose) {
          if (Navigator.of(context).canPop()) {
            Navigator.of(context).pop();
          }
          if (!mounted) return;
          bloc.add(const OutboundCollectionCloseAcknowledged());
        }
        if (state.navigationTarget != null) {
          final Map<String, dynamic> args = <String, dynamic>{};
          if (state.navigationArguments != null) {
            args.addAll(state.navigationArguments!);
          }
          args['bloc'] = bloc;
          switch (state.navigationTarget!) {
            case OutboundNavigation.detail:
              await Modular.to.pushNamed('./detail', arguments: args);
              break;
            case OutboundNavigation.exception:
              Modular.to.pushNamed('./exception', arguments: args);
              break;
            case OutboundNavigation.supplier:
              Modular.to.pushNamed('./supplier', arguments: args);
              break;
          }
          if (!mounted) return;
          bloc.add(const OutboundCollectionNavigationCleared());

        }
      },
      child: BlocBuilder<OutboundCollectionBloc, OutboundCollectionState>(
        builder: (BuildContext context, OutboundCollectionState state) {
          if (state.status == OutboundCollectionStatus.loading ||
              state.status == OutboundCollectionStatus.initial) {
            return const Scaffold(
              body: Center(child: CircularProgressIndicator()),
            );
          }
          if (state.status == OutboundCollectionStatus.failure) {
            return Scaffold(
              appBar: AppBar(title: const Text('出库采集')),
              body: Center(child: Text(state.errorMessage ?? '加载失败')),
            );
          }

          final bloc = context.read<OutboundCollectionBloc>();

          return Scaffold(
            appBar: AppBar(title: const Text('出库采集任务')),
            body: Column(
              children: <Widget>[
                if (state.isProcessing)
                  const LinearProgressIndicator(minHeight: 2),
                Padding(
                  padding: const EdgeInsets.all(12),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: <Widget>[
                      Text(state.messageLabel,
                          style: Theme.of(context).textTheme.titleMedium),
                      const SizedBox(height: 8),
                      Wrap(
                        spacing: 12,
                        runSpacing: 8,
                        children: <Widget>[
                          _InfoChip(label: '库位', value: state.storeSite),
                          _InfoChip(label: '物料', value: state.matCode),
                          _InfoChip(label: '名称', value: state.matName),
                          _InfoChip(label: '批次', value: state.batchNo),
                          _InfoChip(label: '序列', value: state.sn),
                          _InfoChip(
                            label: '数量',
                            value: state.quantityInput?.toString() ?? '',
                          ),
                          _InfoChip(
                            label: '可用库存',
                            value: state.inventoryQty?.toString() ?? '',
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 12),
                  child: Row(
                    children: <Widget>[
                      Expanded(
                        child: TextField(
                          controller: _barcodeController,
                          focusNode: _barcodeFocus,
                          decoration: const InputDecoration(
                            labelText: '扫描条码/库位/数量',
                          ),
                          onSubmitted: (String value) {
                            if (value.isNotEmpty) {
                              bloc.add(
                                  OutboundCollectionBarcodeScanned(value));
                              _barcodeController.clear();
                              _barcodeFocus.requestFocus();
                            }
                          },
                        ),
                      ),
                      const SizedBox(width: 8),
                      ElevatedButton(
                        onPressed: () {
                          final String value = _barcodeController.text.trim();
                          if (value.isNotEmpty) {
                            bloc.add(OutboundCollectionBarcodeScanned(value));
                            _barcodeController.clear();
                            _barcodeFocus.requestFocus();
                          }
                        },
                        child: const Text('确定'),
                      ),
                    ],
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
                  child: Wrap(
                    spacing: 12,
                    runSpacing: 8,
                    children: <Widget>[
                      ElevatedButton(
                        onPressed: () => bloc
                            .add(const OutboundCollectionSubmitRequested()),
                        child: const Text('提交采集'),
                      ),
                      OutlinedButton(
                        onPressed: () => bloc
                            .add(const OutboundCollectionDetailOpened()),
                        child: const Text('采集明细'),
                      ),
                      OutlinedButton(
                        onPressed: () =>
                            bloc.add(const OutboundCollectionFinishSelected()),
                        child: const Text('完成选中'),
                      ),
                      OutlinedButton(
                        onPressed: () =>
                            bloc.add(const OutboundCollectionExceptionOpened()),
                        child: const Text('异常登记'),
                      ),
                      OutlinedButton(
                        onPressed: () =>
                            bloc.add(const OutboundCollectionSupplierOpened()),
                        child: const Text('供应商任务'),
                      ),
                      OutlinedButton(
                        onPressed: () =>
                            bloc.add(const OutboundCollectionCloseRequested()),
                        child: const Text('关闭'),
                      ),
                    ],
                  ),
                ),
                Expanded(
                  child: Row(
                    children: <Widget>[
                      Expanded(
                        child: Padding(
                          padding: const EdgeInsets.all(12),
                          child: _TaskList(
                            items: state.filteredItems,
                            selected: state.selectedItemIds,
                            onToggle: (String id) => bloc
                                .add(OutboundCollectionSelectionToggled(id)),
                          ),
                        ),
                      ),
                      Expanded(
                        child: Padding(
                          padding: const EdgeInsets.all(12),
                          child: _CollectionList(records: state.collectionRecords),
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}

class _TaskList extends StatelessWidget {
  const _TaskList({
    required this.items,
    required this.selected,
    required this.onToggle,
  });

  final List<OutboundTaskItem> items;
  final Set<String> selected;
  final ValueChanged<String> onToggle;

  @override
  Widget build(BuildContext context) {
    if (items.isEmpty) {
      return const Center(child: Text('无任务明细'));
    }
    return ListView.builder(
      itemCount: items.length,
      itemBuilder: (BuildContext context, int index) {
        final item = items[index];
        return TaskItemTile(
          item: item,
          selected: selected.contains(item.outTaskItemId),
          onSelected: (_) => onToggle(item.outTaskItemId),
        );
      },
    );
  }
}

class _CollectionList extends StatelessWidget {
  const _CollectionList({required this.records});

  final List<OutboundCollectionRecord> records;

  @override
  Widget build(BuildContext context) {
    if (records.isEmpty) {
      return const Center(child: Text('暂无采集记录'));
    }
    return ListView.builder(
      itemCount: records.length,
      itemBuilder: (BuildContext context, int index) {
        return CollectionRecordTile(record: records[index]);
      },
    );
  }
}

class _InfoChip extends StatelessWidget {
  const _InfoChip({required this.label, required this.value});

  final String label;
  final String value;

  @override
  Widget build(BuildContext context) {
    return Chip(
      label: Text('$label: $value'),
    );
  }
}
