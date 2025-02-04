namespace InverterReaderService.Models
{
    public class InverterData
    {
        public float GridVoltage { get; set; }
        public float GridFrequency { get; set; }
        public float ACOutputVoltage { get; set; }
        public float ACOutputFrequency { get; set; }
        public int ACOutputApparentPower { get; set; }
        public int ACOutputActivePower { get; set; }
        public int OutputLoadPercent { get; set; }
        public int BusVoltage { get; set; }
        public float BatteryVoltage { get; set; }
        public int BatteryChargingCurrent { get; set; }
        public int BatteryCapacity { get; set; }
        public int InverterHeatsinkTemperature { get; set; }
        public float PVInputCurrent { get; set; }
        public float PVInputVoltage { get; set; }
        public float BatteryVoltageFromSCC { get; set; }
        public int BatteryDischargeCurrent { get; set; }
    }
}