// GENERATED CODE - MANUAL IMPLEMENTATION

part of 'outbound_collection_record.dart';

class OutboundCollectionRecordAdapter extends TypeAdapter<OutboundCollectionRecord> {
  @override
  final int typeId = 41;

  @override
  OutboundCollectionRecord read(BinaryReader reader) {
    final fields = <int, dynamic>{};
    final fieldCount = reader.readByte();
    for (var i = 0; i < fieldCount; i++) {
      final key = reader.readByte();
      fields[key] = reader.read();
    }
    return OutboundCollectionRecord(
      matCode: fields[0] as String,
      matName: fields[1] as String,
      batchNo: fields[2] as String,
      sn: fields[3] as String,
      collectQty: (fields[4] as num).toDouble(),
      taskQty: (fields[5] as num).toDouble(),
      storeRoom: fields[6] as String,
      storeSite: fields[7] as String,
      outTaskItemId: fields[8] as String,
      erpStore: fields[9] as String,
      trayNo: fields[10] as String,
    );
  }

  @override
  void write(BinaryWriter writer, OutboundCollectionRecord obj) {
    writer
      ..writeByte(11)
      ..writeByte(0)
      ..write(obj.matCode)
      ..writeByte(1)
      ..write(obj.matName)
      ..writeByte(2)
      ..write(obj.batchNo)
      ..writeByte(3)
      ..write(obj.sn)
      ..writeByte(4)
      ..write(obj.collectQty)
      ..writeByte(5)
      ..write(obj.taskQty)
      ..writeByte(6)
      ..write(obj.storeRoom)
      ..writeByte(7)
      ..write(obj.storeSite)
      ..writeByte(8)
      ..write(obj.outTaskItemId)
      ..writeByte(9)
      ..write(obj.erpStore)
      ..writeByte(10)
      ..write(obj.trayNo);
  }
}
