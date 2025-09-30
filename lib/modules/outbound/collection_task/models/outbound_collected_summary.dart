import 'package:equatable/equatable.dart';

/// Mirrors the `dicMtlQty` structure in the PDA code.
class OutboundCollectedSummary extends Equatable {
  const OutboundCollectedSummary({
    required this.originalCollectedQty,
    required this.currentCollectedQty,
    required this.matCode,
  });

  OutboundCollectedSummary copyWith({
    double? originalCollectedQty,
    double? currentCollectedQty,
  }) {
    return OutboundCollectedSummary(
      originalCollectedQty: originalCollectedQty ?? this.originalCollectedQty,
      currentCollectedQty: currentCollectedQty ?? this.currentCollectedQty,
      matCode: matCode,
    );
  }

  final double originalCollectedQty;
  final double currentCollectedQty;
  final String matCode;

  @override
  List<Object?> get props => <Object?>[originalCollectedQty, currentCollectedQty, matCode];
}
