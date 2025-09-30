import 'package:equatable/equatable.dart';

class OutboundInventoryRecord extends Equatable {
  const OutboundInventoryRecord({
    required this.storeSite,
    required this.matCode,
    required this.batchNo,
    required this.sn,
    required this.erpStore,
    required this.quantity,
  });

  factory OutboundInventoryRecord.fromJson(Map<String, dynamic> json) {
    double parseQty(dynamic value) {
      if (value == null || value.toString().trim().isEmpty) {
        return 0;
      }
      return double.tryParse(value.toString()) ?? 0;
    }

    return OutboundInventoryRecord(
      storeSite: (json['storesiteno'] ?? json['storeSiteNo'] ?? '').toString(),
      matCode: (json['matcode'] ?? json['matCode'] ?? '').toString(),
      batchNo: (json['batchno'] ?? json['batchNo'] ?? '').toString(),
      sn: (json['sn'] ?? '').toString(),
      erpStore: (json['erp_storeroom'] ?? json['erpStore'] ?? json['erpStoreroom'] ?? '').toString(),
      quantity: parseQty(json['quantity'] ?? json['repqty'] ?? json['qty']),
    );
  }

  final String storeSite;
  final String matCode;
  final String batchNo;
  final String sn;
  final String erpStore;
  final double quantity;

  @override
  List<Object?> get props => <Object?>[storeSite, matCode, batchNo, sn, erpStore, quantity];
}
