import 'package:equatable/equatable.dart';

class DownShelvesInfoPayload extends Equatable {
  const DownShelvesInfoPayload({
    required this.taskNo,
    required this.matCode,
    required this.batchNo,
    required this.sn,
    required this.collectQty,
    required this.storeRoomNo,
    required this.storeSiteNo,
    required this.outTaskItemId,
    required this.erpStore,
    required this.trayNo,
  });

  Map<String, dynamic> toJson() {
    return <String, dynamic>{
      'outtaskid': taskNo,
      'matcode': matCode,
      'batchno': batchNo,
      'sn': sn,
      'quantity': collectQty,
      'storeroomno': storeRoomNo,
      'storesiteno': storeSiteNo,
      'outtaskitemid': outTaskItemId,
      'erpstoreroom': erpStore,
      'palletno': trayNo,
    };
  }

  final String taskNo;
  final String matCode;
  final String batchNo;
  final String sn;
  final double collectQty;
  final String storeRoomNo;
  final String storeSiteNo;
  final String outTaskItemId;
  final String erpStore;
  final String trayNo;

  @override
  List<Object?> get props => <Object?>[
        taskNo,
        matCode,
        batchNo,
        sn,
        collectQty,
        storeRoomNo,
        storeSiteNo,
        outTaskItemId,
        erpStore,
        trayNo,
      ];
}

class ItemListInfoPayload extends Equatable {
  const ItemListInfoPayload({
    required this.outTaskItemId,
    required this.originalQty,
    required this.collectedQty,
    required this.matCode,
  });

  Map<String, dynamic> toJson() {
    return <String, dynamic>{
      'outtaskitemid': outTaskItemId,
      'quantity': <String>[originalQty.toString(), collectedQty.toString()],
      'matcode': matCode,
    };
  }

  final String outTaskItemId;
  final double originalQty;
  final double collectedQty;
  final String matCode;

  @override
  List<Object?> get props => <Object?>[outTaskItemId, originalQty, collectedQty, matCode];
}
