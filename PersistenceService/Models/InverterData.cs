using System;

namespace PersistenceService.Models
{
    public class InverterData
    {
        public int Id { get; set; }

        public float Grid_voltage { get; set; }
        public float Grid_frequency { get; set; }
        public float AC_output_voltage { get; set; }
        public float AC_output_frequency { get; set; }
        public float AC_output_apparent_power { get; set; }
        public float AC_output_active_power { get; set; }
        public float Output_Load_Percent { get; set; }
        public float Bus_voltage { get; set; }
        public float Battery_voltage { get; set; }
        public float Battery_charging_current { get; set; }
        public float Battery_capacity { get; set; }
        public float Inverter_heatsink_temperature { get; set; }
        public float PV_input_current_for_battery { get; set; }
        public float PV_Input_Voltage { get; set; }
        public float Battery_voltage_from_SCC { get; set; }
        public float Battery_discharge_current { get; set; }
        public string Status {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}