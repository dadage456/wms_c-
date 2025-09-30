import 'package:equatable/equatable.dart';

/// Information returned by `/system/terminal/getMatControl`.
class MatControlInfo extends Equatable {
  const MatControlInfo({
    required this.controlType,
    required this.matId,
    required this.sendControl,
  });

  factory MatControlInfo.fromRaw(String raw) {
    final parts = raw.split('!');
    final int control = parts.isNotEmpty && parts.first.trim().isNotEmpty
        ? int.parse(parts.first.trim())
        : 0;
    final String id = parts.length > 3 ? parts[3] : '';
    final String send = parts.length > 4 && parts[4].trim().isNotEmpty ? parts[4] : '0';
    return MatControlInfo(controlType: control, matId: id, sendControl: send);
  }

  factory MatControlInfo.fromJson(Map<String, dynamic> json) {
    final int control = int.tryParse((json['batchcontrol'] ?? json['controlType'] ?? '0').toString()) ?? 0;
    final String id = (json['matid'] ?? json['matId'] ?? '').toString();
    final String send = (json['sendcontrol'] ?? json['sendControl'] ?? json['matSendControl'] ?? '0').toString();
    return MatControlInfo(controlType: control, matId: id, sendControl: send);
  }

  final int controlType; // 0: SN, 1: Batch, 2: Batch+Site
  final String matId;
  final String sendControl;

  @override
  List<Object?> get props => <Object?>[controlType, matId, sendControl];
}
