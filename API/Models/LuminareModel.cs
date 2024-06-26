using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class LuminareModel : BaseModel
    {
        public long Address { get; set; }
        public byte Maintained { get; set; }
        public byte BatteryCharging { get; set; }
        public byte BatteryTestRunning { get; set; }
        public byte LampTestRunning { get; set; }
        public byte BatteryCapacityFault { get; set; }
        public byte LampFault { get; set; }
        public byte ChargerFault { get; set; }
        public byte MainsFault { get; set; }
        public string BatteryVoltage { get; set; } = string.Empty;
        public string ChargeCurrent { get; set; } = string.Empty;
        public string LampCurrent { get; set; } = string.Empty;
        public string Autonomy { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string LuminaryModel { get; set; } = string.Empty;
        public byte BatteryFault { get; set; }
        public byte BatteryCutOffStatus { get; set; }
        public byte SpiCommError { get; set; }
        public byte ResolvedSpiCommError { get; set; }
        public string ModuleVersion { get; set; } = string.Empty; 
        public int ModuleType { get; set; }
        public string TimeToSend { get; set; } = string.Empty;
        public byte HasWrongLuminaireType { get; set; }
        public byte FullyCharged { get; set; }
        public long Rfchannel { get; set; }
        public byte LampTestMade { get; set; }
        public string DimmingLevel { get; set; } = string.Empty;
        public int Zone { get; set; }
        public long SidToGo { get; set; }
        public long NetworkidToGo { get; set; }
        public int RfchannelToGo { get; set; }
        public byte InForcedEmergency { get; set; }
        public int Rssi { get; set; }
        public string RssiWithUnit { get; set; } = string.Empty;
        public long NetworkLevel { get; set; }
        public long HopCounter { get; set; }
        public long MessageCounter { get; set; }
        public long LatencyCounter { get; set; }
        public string Rc1181fwVersion { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdateOn { get; set; } = DateTime.Now;
    }
}