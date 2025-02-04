using System;
using System.Collections.Generic;

public class InverterStatusParser
{
    private static readonly Dictionary<char, string> StatusMap = new()
    {
        { 'L', "Line Mode" },
        { 'B', "Battery Mode" },
        { 'S', "Standby Mode" },
        { 'F', "Fault Mode" },
        { 'P', "Power Saving Mode" },
        { 'H', "Hybrid Mode" },
        { 'D', "Bypass Mode" }
    };

    public static string ParseInverterStatus(string response)
    {
        if (string.IsNullOrWhiteSpace(response))
            return "Invalid response";

        // Pulizia della risposta: rimuove eventuali spazi o newline
        string cleanedResponse = response.Trim();

        // Verifica che la risposta sia almeno di lunghezza 3 (ad esempio "(L")
        if (cleanedResponse.Length < 3 || cleanedResponse[0] != '(')
            return "Invalid response format";

        // Estrai il carattere di stato
        char statusCode = cleanedResponse[1];

        // Restituisce il valore corrispondente alla mappa
        return StatusMap.TryGetValue(statusCode, out string status) ? status : "Unknown status";
    }
}