import 'package:equatable/equatable.dart';

/// Parsed content from 2D barcode. Mirrors the PDA `BarcodeContent` entity.
class BarcodeContent extends Equatable {
  const BarcodeContent({
    required this.matCode,
    required this.batchNo,
    required this.sn,
    required this.packageQty,
    required this.color,
    required this.specificAttribute,
    required this.drawCode,
    required this.drawVersion,
    required this.manufacturerCode,
    required this.agentCode,
  });

  factory BarcodeContent.fromJson(Map<String, dynamic> json) {
    return BarcodeContent(
      matCode: (json['matcode'] ?? json['matCode'] ?? '').toString(),
      batchNo: (json['batchno'] ?? json['batchNo'] ?? '').toString(),
      sn: (json['sn'] ?? json['SN'] ?? '').toString(),
      packageQty: (json['packageqty'] ?? json['packageQty'] ?? '').toString(),
      color: (json['color'] ?? '').toString(),
      specificAttribute: (json['specificAttribute'] ?? json['specificattribute'] ?? '').toString(),
      drawCode: (json['drawCode'] ?? json['drawcode'] ?? '').toString(),
      drawVersion: (json['drawVersion'] ?? json['drawversion'] ?? '').toString(),
      manufacturerCode: (json['manufacturerCode'] ?? json['manufacturercode'] ?? '').toString(),
      agentCode: (json['aagentCode'] ?? json['agentCode'] ?? json['aagentcode'] ?? '').toString(),
    );
  }

  final String matCode;
  final String batchNo;
  final String sn;
  final String packageQty;
  final String color;
  final String specificAttribute;
  final String drawCode;
  final String drawVersion;
  final String manufacturerCode;
  final String agentCode;

  @override
  List<Object?> get props => <Object?>[
        matCode,
        batchNo,
        sn,
        packageQty,
        color,
        specificAttribute,
        drawCode,
        drawVersion,
        manufacturerCode,
        agentCode,
      ];
}
