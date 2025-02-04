using System;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class InverterData
    {
        public int Id { get; set; }

        [JsonPropertyName("grid_voltage")]
        public double GridVoltage { get; set; }

        [JsonPropertyName("grid_frequency")]
        public double GridFrequency { get; set; }

        [JsonPropertyName("aC_output_voltage")]
        public double ACOutputVoltage { get; set; }

        [JsonPropertyName("aC_output_frequency")]
        public double ACOutputFrequency { get; set; }

        [JsonPropertyName("aC_output_apparent_power")]
        public int ACOutputApparentPower { get; set; }

        [JsonPropertyName("aC_output_active_power")]
        public int ACOutputActivePower { get; set; }

        [JsonPropertyName("output_Load_Percent")]
        public int OutputLoadPercent { get; set; }

        [JsonPropertyName("bus_voltage")]
        public int BusVoltage { get; set; }

        [JsonPropertyName("battery_voltage")]
        public double BatteryVoltage { get; set; }

        [JsonPropertyName("battery_charging_current")]
        public int BatteryChargingCurrent { get; set; }

        [JsonPropertyName("battery_capacity")]
        public int BatteryCapacity { get; set; }

        [JsonPropertyName("inverter_heatsink_temperature")]
        public int InverterHeatsinkTemperature { get; set; }

        [JsonPropertyName("pV_input_current_for_battery")]
        public double PVInputCurrentForBattery { get; set; }

        [JsonPropertyName("pV_Input_Voltage")]
        public double PVInputVoltage { get; set; }

        [JsonPropertyName("battery_voltage_from_SCC")]
        public int BatteryVoltageFromSCC { get; set; }

        [JsonPropertyName("battery_discharge_current")]
        public int BatteryDischargeCurrent { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}