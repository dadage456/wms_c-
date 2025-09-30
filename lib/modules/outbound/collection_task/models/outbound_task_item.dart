import 'package:equatable/equatable.dart';

class OutboundTaskItem extends Equatable {
  const OutboundTaskItem({
    required this.outTaskItemId,
    required this.matCode,
    required this.matName,
    required this.storeSite,
    required this.storeRoom,
    required this.hintQty,
    required this.collectedQty,
    required this.repertoryQty,
    required this.batchNo,
    required this.sn,
    required this.erpStore,
    required this.orderNo,
    required this.matInnerCode,
    required this.trayNo,
  });

  factory OutboundTaskItem.fromJson(Map<String, dynamic> json) {
    double parseDouble(dynamic value) {
      if (value == null || value.toString().isEmpty) {
        return 0;
      }
      return double.tryParse(value.toString()) ?? 0;
    }

    return OutboundTaskItem(
      outTaskItemId: (json['outtaskitemid'] ?? json['outTaskItemId'] ?? '').toString(),
      matCode: (json['matcode'] ?? json['matCode'] ?? '').toString(),
      matName: (json['matname'] ?? json['matName'] ?? '').toString(),
      storeSite: (json['storesiteno'] ?? json['storeSiteNo'] ?? '').toString(),
      storeRoom: (json['storeroomno'] ?? json['storeRoomNo'] ?? '').toString(),
      hintQty: parseDouble(json['quantity'] ?? json['hintqty'] ?? json['taskQty']),
      collectedQty: parseDouble(json['collectedqty'] ?? json['collectQty'] ?? json['collectedQty']),
      repertoryQty: parseDouble(json['repqty'] ?? json['repertoryQty'] ?? 0),
      batchNo: (json['batchno'] ?? json['batchNo'] ?? '').toString(),
      sn: (json['sn'] ?? '').toString(),
      erpStore: (json['erp_storeroom'] ?? json['erpStore'] ?? json['erpStoreroom'] ?? '').toString(),
      orderNo: (json['orderno'] ?? json['orderNo'] ?? '').toString(),
      matInnerCode: (json['matinnercode'] ?? json['matInnerCode'] ?? '').toString(),
      trayNo: (json['palletno'] ?? json['trayNo'] ?? '').toString(),
    );
  }

  OutboundTaskItem copyWith({
    double? hintQty,
    double? collectedQty,
    double? repertoryQty,
    String? storeSite,
  }) {
    return OutboundTaskItem(
      outTaskItemId: outTaskItemId,
      matCode: matCode,
      matName: matName,
      storeSite: storeSite ?? this.storeSite,
      storeRoom: storeRoom,
      hintQty: hintQty ?? this.hintQty,
      collectedQty: collectedQty ?? this.collectedQty,
      repertoryQty: repertoryQty ?? this.repertoryQty,
      batchNo: batchNo,
      sn: sn,
      erpStore: erpStore,
      orderNo: orderNo,
      matInnerCode: matInnerCode,
      trayNo: trayNo,
    );
  }

  final String outTaskItemId;
  final String matCode;
  final String matName;
  final String storeSite;
  final String storeRoom;
  final double hintQty;
  final double collectedQty;
  final double repertoryQty;
  final String batchNo;
  final String sn;
  final String erpStore;
  final String orderNo;
  final String matInnerCode;
  final String trayNo;

  @override
  List<Object?> get props => <Object?>[
        outTaskItemId,
        matCode,
        matName,
        storeSite,
        storeRoom,
        hintQty,
        collectedQty,
        repertoryQty,
        batchNo,
        sn,
        erpStore,
        orderNo,
        matInnerCode,
        trayNo,
      ];
}
