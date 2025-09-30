import 'package:equatable/equatable.dart';
import 'package:hive/hive.dart';

part 'outbound_collection_record.g.dart';

@HiveType(typeId: 41)
class OutboundCollectionRecord extends HiveObject with EquatableMixin {
  OutboundCollectionRecord({
    required this.matCode,
    required this.matName,
    required this.batchNo,
    required this.sn,
    required this.collectQty,
    required this.taskQty,
    required this.storeRoom,
    required this.storeSite,
    required this.outTaskItemId,
    required this.erpStore,
    required this.trayNo,
  });

  @HiveField(0)
  String matCode;

  @HiveField(1)
  String matName;

  @HiveField(2)
  String batchNo;

  @HiveField(3)
  String sn;

  @HiveField(4)
  double collectQty;

  @HiveField(5)
  double taskQty;

  @HiveField(6)
  String storeRoom;

  @HiveField(7)
  String storeSite;

  @HiveField(8)
  String outTaskItemId;

  @HiveField(9)
  String erpStore;

  @HiveField(10)
  String trayNo;

  @override
  List<Object?> get props => <Object?>[
        matCode,
        matName,
        batchNo,
        sn,
        collectQty,
        taskQty,
        storeRoom,
        storeSite,
        outTaskItemId,
        erpStore,
        trayNo,
      ];
}
