part of 'outbound_collection_bloc.dart';

abstract class OutboundCollectionEvent extends Equatable {
  const OutboundCollectionEvent();

  @override
  List<Object?> get props => const <Object?>[];
}

class _OutboundCollectionInitialized extends OutboundCollectionEvent {
  const _OutboundCollectionInitialized();
}

class OutboundCollectionBarcodeScanned extends OutboundCollectionEvent {
  const OutboundCollectionBarcodeScanned(this.raw);

  final String raw;

  @override
  List<Object?> get props => <Object?>[raw];
}

class OutboundCollectionSubmitRequested extends OutboundCollectionEvent {
  const OutboundCollectionSubmitRequested();
}

class OutboundCollectionPendingActionConfirmed extends OutboundCollectionEvent {
  const OutboundCollectionPendingActionConfirmed(this.confirmed);

  final bool confirmed;

  @override
  List<Object?> get props => <Object?>[confirmed];
}

class OutboundCollectionCloseRequested extends OutboundCollectionEvent {
  const OutboundCollectionCloseRequested();
}

class OutboundCollectionSelectionToggled extends OutboundCollectionEvent {
  const OutboundCollectionSelectionToggled(this.outTaskItemId);

  final String outTaskItemId;

  @override
  List<Object?> get props => <Object?>[outTaskItemId];
}

class OutboundCollectionFinishSelected extends OutboundCollectionEvent {
  const OutboundCollectionFinishSelected();
}

class OutboundCollectionSuccessCleared extends OutboundCollectionEvent {
  const OutboundCollectionSuccessCleared();
}

class OutboundCollectionErrorCleared extends OutboundCollectionEvent {
  const OutboundCollectionErrorCleared();
}

class OutboundCollectionDetailOpened extends OutboundCollectionEvent {
  const OutboundCollectionDetailOpened();
}

class OutboundCollectionExceptionOpened extends OutboundCollectionEvent {
  const OutboundCollectionExceptionOpened();
}

class OutboundCollectionSupplierOpened extends OutboundCollectionEvent {
  const OutboundCollectionSupplierOpened();
}

class OutboundCollectionNavigationCleared extends OutboundCollectionEvent {
  const OutboundCollectionNavigationCleared();
}

class OutboundCollectionCloseAcknowledged extends OutboundCollectionEvent {
  const OutboundCollectionCloseAcknowledged();
}

class OutboundCollectionRecordDeleted extends OutboundCollectionEvent {
  const OutboundCollectionRecordDeleted(this.record);

  final OutboundCollectionRecord record;

  @override
  List<Object?> get props => <Object?>[record];
}
