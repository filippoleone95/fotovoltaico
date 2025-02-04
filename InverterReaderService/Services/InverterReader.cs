using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

public class InverterDataParser
{
    public static Dictionary<string, object> ParseInverterData(string response)
    {
        //Console.WriteLine($"Risposta ricevuta: {response}");
        // Rimuovi caratteri non numerici (eccetto spazi e punti) e token anomali
        string cleanedResponse = Regex.Replace(response, @"[^0-9.\s]", "").Trim();

        // Dividi i dati in valori individuali
        string[] values = cleanedResponse.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //Console.WriteLine($"Valori ottenuti: {string.Join(", ", values)}");

        var inverterData = new Dictionary<string, object>();
        try
        {
            if(values.Length < 15)
            {
                throw new FormatException("Il numero di valori ricevuti non è sufficiente.");
            }
            else {
                // Associa i valori alle chiavi corrispondenti basate sul manuale
                inverterData = new Dictionary<string, object>
                {
                    { "Grid_voltage", GetFloat(values, 0) },
                    { "Grid_frequency", GetFloat(values, 1) },
                    { "AC_output_voltage", GetFloat(values, 2) },
                    { "AC_output_frequency", GetFloat(values, 3) },
                    { "AC_output_apparent_power", GetInt(values, 4) },
                    { "AC_output_active_power", GetInt(values, 5) },
                    { "Output_Load_Percent", GetInt(values, 6) },
                    { "Bus_voltage", GetInt(values, 7) },
                    { "Battery_voltage", GetFloat(values, 8) },
                    { "Battery_charging_current", GetInt(values, 9) },
                    { "Battery_capacity", GetInt(values, 10) },
                    { "Inverter_heatsink_temperature", GetInt(values, 11) },
                    { "PV_input_current_for_battery", GetFloat(values, 12) },
                    { "PV_Input_Voltage", GetFloat(values, 13) },
                    { "Battery_voltage_from_SCC", GetFloat(values, 14) },
                    { "Battery_discharge_current", GetInt(values, 15) }
                };
            }
        }
        catch (Exception ex)
        {
            // Gestisce errori di parsing e restituisce un messaggio dettagliato
            Console.WriteLine($"Errore durante il parsing dei dati: {ex.Message}");
        }
        
        return inverterData;
    }

    private static float GetFloat(string[] values, int index)
    {
        if (index < values.Length && float.TryParse(values[index], NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
        {
            return result;
        }
        throw new FormatException($"Il valore alla posizione {index} non può essere convertito in float.");
    }

    private static int GetInt(string[] values, int index)
    {
        if (index < values.Length && int.TryParse(values[index], NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
        {
            return result;
        }
        throw new FormatException($"Il valore alla posizione {index} non può essere convertito in int.");
    }
}