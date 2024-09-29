// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Temp/Proto/Table_ItemQuality.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Temp/Proto/Table_ItemQuality.proto</summary>
public static partial class TableItemQualityReflection {

  #region Descriptor
  /// <summary>File descriptor for Temp/Proto/Table_ItemQuality.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static TableItemQualityReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CiJUZW1wL1Byb3RvL1RhYmxlX0l0ZW1RdWFsaXR5LnByb3RvIrUBCg9Sb3df",
          "SXRlbVF1YWxpdHkSCgoCaWQYASABKAUSDQoFY29sb3IYAiABKAkSDgoGaWNv",
          "bkJnGAMgASgJEhEKCWljb25GcmFtZRgEIAEoCRIQCghlZmZlY3RJZBgFIAEo",
          "BRIRCglseVF1YWxpdHkYBiABKAkSEwoLZXF1aXBTb3VsQmcYByABKAkSFgoO",
          "ZXF1aXBTb3VsRnJhbWUYCCABKAkSEgoKRnJhZ21lbnRCZxgJIAEoCSI0ChFU",
          "YWJsZV9JdGVtUXVhbGl0eRIfCgVkYXRhcxgBIAMoCzIQLlJvd19JdGVtUXVh",
          "bGl0eQ=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Row_ItemQuality), global::Row_ItemQuality.Parser, new[]{ "Id", "Color", "IconBg", "IconFrame", "EffectId", "LyQuality", "EquipSoulBg", "EquipSoulFrame", "FragmentBg" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::Table_ItemQuality), global::Table_ItemQuality.Parser, new[]{ "Datas" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class Row_ItemQuality : pb::IMessage<Row_ItemQuality>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<Row_ItemQuality> _parser = new pb::MessageParser<Row_ItemQuality>(() => new Row_ItemQuality());
  private pb::UnknownFieldSet _unknownFields;
  private int _hasBits0;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<Row_ItemQuality> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::TableItemQualityReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Row_ItemQuality() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Row_ItemQuality(Row_ItemQuality other) : this() {
    _hasBits0 = other._hasBits0;
    id_ = other.id_;
    color_ = other.color_;
    iconBg_ = other.iconBg_;
    iconFrame_ = other.iconFrame_;
    effectId_ = other.effectId_;
    lyQuality_ = other.lyQuality_;
    equipSoulBg_ = other.equipSoulBg_;
    equipSoulFrame_ = other.equipSoulFrame_;
    fragmentBg_ = other.fragmentBg_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Row_ItemQuality Clone() {
    return new Row_ItemQuality(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private readonly static int IdDefaultValue = 0;

  private int id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int Id {
    get { if ((_hasBits0 & 1) != 0) { return id_; } else { return IdDefaultValue; } }
    set {
      _hasBits0 |= 1;
      id_ = value;
    }
  }
  /// <summary>Gets whether the "id" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasId {
    get { return (_hasBits0 & 1) != 0; }
  }
  /// <summary>Clears the value of the "id" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearId() {
    _hasBits0 &= ~1;
  }

  /// <summary>Field number for the "color" field.</summary>
  public const int ColorFieldNumber = 2;
  private readonly static string ColorDefaultValue = "";

  private string color_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Color {
    get { return color_ ?? ColorDefaultValue; }
    set {
      color_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "color" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasColor {
    get { return color_ != null; }
  }
  /// <summary>Clears the value of the "color" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearColor() {
    color_ = null;
  }

  /// <summary>Field number for the "iconBg" field.</summary>
  public const int IconBgFieldNumber = 3;
  private readonly static string IconBgDefaultValue = "";

  private string iconBg_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string IconBg {
    get { return iconBg_ ?? IconBgDefaultValue; }
    set {
      iconBg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "iconBg" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasIconBg {
    get { return iconBg_ != null; }
  }
  /// <summary>Clears the value of the "iconBg" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearIconBg() {
    iconBg_ = null;
  }

  /// <summary>Field number for the "iconFrame" field.</summary>
  public const int IconFrameFieldNumber = 4;
  private readonly static string IconFrameDefaultValue = "";

  private string iconFrame_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string IconFrame {
    get { return iconFrame_ ?? IconFrameDefaultValue; }
    set {
      iconFrame_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "iconFrame" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasIconFrame {
    get { return iconFrame_ != null; }
  }
  /// <summary>Clears the value of the "iconFrame" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearIconFrame() {
    iconFrame_ = null;
  }

  /// <summary>Field number for the "effectId" field.</summary>
  public const int EffectIdFieldNumber = 5;
  private readonly static int EffectIdDefaultValue = 0;

  private int effectId_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int EffectId {
    get { if ((_hasBits0 & 2) != 0) { return effectId_; } else { return EffectIdDefaultValue; } }
    set {
      _hasBits0 |= 2;
      effectId_ = value;
    }
  }
  /// <summary>Gets whether the "effectId" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasEffectId {
    get { return (_hasBits0 & 2) != 0; }
  }
  /// <summary>Clears the value of the "effectId" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearEffectId() {
    _hasBits0 &= ~2;
  }

  /// <summary>Field number for the "lyQuality" field.</summary>
  public const int LyQualityFieldNumber = 6;
  private readonly static string LyQualityDefaultValue = "";

  private string lyQuality_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string LyQuality {
    get { return lyQuality_ ?? LyQualityDefaultValue; }
    set {
      lyQuality_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "lyQuality" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasLyQuality {
    get { return lyQuality_ != null; }
  }
  /// <summary>Clears the value of the "lyQuality" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearLyQuality() {
    lyQuality_ = null;
  }

  /// <summary>Field number for the "equipSoulBg" field.</summary>
  public const int EquipSoulBgFieldNumber = 7;
  private readonly static string EquipSoulBgDefaultValue = "";

  private string equipSoulBg_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string EquipSoulBg {
    get { return equipSoulBg_ ?? EquipSoulBgDefaultValue; }
    set {
      equipSoulBg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "equipSoulBg" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasEquipSoulBg {
    get { return equipSoulBg_ != null; }
  }
  /// <summary>Clears the value of the "equipSoulBg" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearEquipSoulBg() {
    equipSoulBg_ = null;
  }

  /// <summary>Field number for the "equipSoulFrame" field.</summary>
  public const int EquipSoulFrameFieldNumber = 8;
  private readonly static string EquipSoulFrameDefaultValue = "";

  private string equipSoulFrame_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string EquipSoulFrame {
    get { return equipSoulFrame_ ?? EquipSoulFrameDefaultValue; }
    set {
      equipSoulFrame_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "equipSoulFrame" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasEquipSoulFrame {
    get { return equipSoulFrame_ != null; }
  }
  /// <summary>Clears the value of the "equipSoulFrame" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearEquipSoulFrame() {
    equipSoulFrame_ = null;
  }

  /// <summary>Field number for the "FragmentBg" field.</summary>
  public const int FragmentBgFieldNumber = 9;
  private readonly static string FragmentBgDefaultValue = "";

  private string fragmentBg_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string FragmentBg {
    get { return fragmentBg_ ?? FragmentBgDefaultValue; }
    set {
      fragmentBg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "FragmentBg" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasFragmentBg {
    get { return fragmentBg_ != null; }
  }
  /// <summary>Clears the value of the "FragmentBg" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearFragmentBg() {
    fragmentBg_ = null;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as Row_ItemQuality);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(Row_ItemQuality other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (Color != other.Color) return false;
    if (IconBg != other.IconBg) return false;
    if (IconFrame != other.IconFrame) return false;
    if (EffectId != other.EffectId) return false;
    if (LyQuality != other.LyQuality) return false;
    if (EquipSoulBg != other.EquipSoulBg) return false;
    if (EquipSoulFrame != other.EquipSoulFrame) return false;
    if (FragmentBg != other.FragmentBg) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (HasId) hash ^= Id.GetHashCode();
    if (HasColor) hash ^= Color.GetHashCode();
    if (HasIconBg) hash ^= IconBg.GetHashCode();
    if (HasIconFrame) hash ^= IconFrame.GetHashCode();
    if (HasEffectId) hash ^= EffectId.GetHashCode();
    if (HasLyQuality) hash ^= LyQuality.GetHashCode();
    if (HasEquipSoulBg) hash ^= EquipSoulBg.GetHashCode();
    if (HasEquipSoulFrame) hash ^= EquipSoulFrame.GetHashCode();
    if (HasFragmentBg) hash ^= FragmentBg.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (HasId) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (HasColor) {
      output.WriteRawTag(18);
      output.WriteString(Color);
    }
    if (HasIconBg) {
      output.WriteRawTag(26);
      output.WriteString(IconBg);
    }
    if (HasIconFrame) {
      output.WriteRawTag(34);
      output.WriteString(IconFrame);
    }
    if (HasEffectId) {
      output.WriteRawTag(40);
      output.WriteInt32(EffectId);
    }
    if (HasLyQuality) {
      output.WriteRawTag(50);
      output.WriteString(LyQuality);
    }
    if (HasEquipSoulBg) {
      output.WriteRawTag(58);
      output.WriteString(EquipSoulBg);
    }
    if (HasEquipSoulFrame) {
      output.WriteRawTag(66);
      output.WriteString(EquipSoulFrame);
    }
    if (HasFragmentBg) {
      output.WriteRawTag(74);
      output.WriteString(FragmentBg);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (HasId) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (HasColor) {
      output.WriteRawTag(18);
      output.WriteString(Color);
    }
    if (HasIconBg) {
      output.WriteRawTag(26);
      output.WriteString(IconBg);
    }
    if (HasIconFrame) {
      output.WriteRawTag(34);
      output.WriteString(IconFrame);
    }
    if (HasEffectId) {
      output.WriteRawTag(40);
      output.WriteInt32(EffectId);
    }
    if (HasLyQuality) {
      output.WriteRawTag(50);
      output.WriteString(LyQuality);
    }
    if (HasEquipSoulBg) {
      output.WriteRawTag(58);
      output.WriteString(EquipSoulBg);
    }
    if (HasEquipSoulFrame) {
      output.WriteRawTag(66);
      output.WriteString(EquipSoulFrame);
    }
    if (HasFragmentBg) {
      output.WriteRawTag(74);
      output.WriteString(FragmentBg);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (HasId) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (HasColor) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Color);
    }
    if (HasIconBg) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(IconBg);
    }
    if (HasIconFrame) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(IconFrame);
    }
    if (HasEffectId) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(EffectId);
    }
    if (HasLyQuality) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(LyQuality);
    }
    if (HasEquipSoulBg) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(EquipSoulBg);
    }
    if (HasEquipSoulFrame) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(EquipSoulFrame);
    }
    if (HasFragmentBg) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FragmentBg);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(Row_ItemQuality other) {
    if (other == null) {
      return;
    }
    if (other.HasId) {
      Id = other.Id;
    }
    if (other.HasColor) {
      Color = other.Color;
    }
    if (other.HasIconBg) {
      IconBg = other.IconBg;
    }
    if (other.HasIconFrame) {
      IconFrame = other.IconFrame;
    }
    if (other.HasEffectId) {
      EffectId = other.EffectId;
    }
    if (other.HasLyQuality) {
      LyQuality = other.LyQuality;
    }
    if (other.HasEquipSoulBg) {
      EquipSoulBg = other.EquipSoulBg;
    }
    if (other.HasEquipSoulFrame) {
      EquipSoulFrame = other.EquipSoulFrame;
    }
    if (other.HasFragmentBg) {
      FragmentBg = other.FragmentBg;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Color = input.ReadString();
          break;
        }
        case 26: {
          IconBg = input.ReadString();
          break;
        }
        case 34: {
          IconFrame = input.ReadString();
          break;
        }
        case 40: {
          EffectId = input.ReadInt32();
          break;
        }
        case 50: {
          LyQuality = input.ReadString();
          break;
        }
        case 58: {
          EquipSoulBg = input.ReadString();
          break;
        }
        case 66: {
          EquipSoulFrame = input.ReadString();
          break;
        }
        case 74: {
          FragmentBg = input.ReadString();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Color = input.ReadString();
          break;
        }
        case 26: {
          IconBg = input.ReadString();
          break;
        }
        case 34: {
          IconFrame = input.ReadString();
          break;
        }
        case 40: {
          EffectId = input.ReadInt32();
          break;
        }
        case 50: {
          LyQuality = input.ReadString();
          break;
        }
        case 58: {
          EquipSoulBg = input.ReadString();
          break;
        }
        case 66: {
          EquipSoulFrame = input.ReadString();
          break;
        }
        case 74: {
          FragmentBg = input.ReadString();
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class Table_ItemQuality : pb::IMessage<Table_ItemQuality>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<Table_ItemQuality> _parser = new pb::MessageParser<Table_ItemQuality>(() => new Table_ItemQuality());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<Table_ItemQuality> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::TableItemQualityReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Table_ItemQuality() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Table_ItemQuality(Table_ItemQuality other) : this() {
    datas_ = other.datas_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public Table_ItemQuality Clone() {
    return new Table_ItemQuality(this);
  }

  /// <summary>Field number for the "datas" field.</summary>
  public const int DatasFieldNumber = 1;
  private static readonly pb::FieldCodec<global::Row_ItemQuality> _repeated_datas_codec
      = pb::FieldCodec.ForMessage(10, global::Row_ItemQuality.Parser);
  private readonly pbc::RepeatedField<global::Row_ItemQuality> datas_ = new pbc::RepeatedField<global::Row_ItemQuality>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<global::Row_ItemQuality> Datas {
    get { return datas_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as Table_ItemQuality);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(Table_ItemQuality other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if(!datas_.Equals(other.datas_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= datas_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    datas_.WriteTo(output, _repeated_datas_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    datas_.WriteTo(ref output, _repeated_datas_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    size += datas_.CalculateSize(_repeated_datas_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(Table_ItemQuality other) {
    if (other == null) {
      return;
    }
    datas_.Add(other.datas_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          datas_.AddEntriesFrom(input, _repeated_datas_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          datas_.AddEntriesFrom(ref input, _repeated_datas_codec);
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
