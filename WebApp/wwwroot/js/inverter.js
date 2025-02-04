// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    function updateBattery() {
        let capacity = parseInt(document.getElementById("capacity").innerText);
        let batteryLevel = document.getElementById("batteryLevel");

        // Aggiorna la larghezza della batteria in base alla capacità
        batteryLevel.style.width = capacity + "%";

        // Cambia colore in base alla percentuale di carica
        if (capacity > 35) {
            batteryLevel.style.backgroundColor = "green";
        } else if (capacity > 20) {
            batteryLevel.style.backgroundColor = "yellow";
        } else {
            batteryLevel.style.backgroundColor = "red";
        }
    }

    updateBattery(); // Esegui l'aggiornamento iniziale
});

document.addEventListener("DOMContentLoaded", function () {
    console.log("Dati Inverter:", inverterData[0]);

    var inverterDati = inverterData[0];

    if (!inverterDati) {
        console.log("Errore: Dati inverter non disponibili");
        return;
    }

    // Estrai i dati dall'oggetto inverterData
    let inverterStatus = inverterDati.Status;  
    let pvPower = inverterDati.PVInputVoltage;       
    let batteryVoltage = inverterDati.BatteryVoltage;  
    let gridVoltage = inverterDati.GridVoltage;   
    let batteryChargingCurrent = inverterDati.BatteryChargingCurrent;
    let batteryDischargeCurrent = inverterDati.BatteryDischargeCurrent;

    console.log("Stato Inverter:", inverterStatus);
    console.log("PV Power:", pvPower);
    console.log("Battery Voltage:", batteryVoltage);
    console.log("Grid Voltage:", gridVoltage);
    console.log("Battery Charging Current:", batteryChargingCurrent);
    console.log("Battery Discharging Current:", batteryDischargeCurrent);

    // --------------------------- GESTISCO LA FORNITURA ELETTRICA ---------------------------

    if (inverterStatus === "Battery Mode" && pvPower > 0) {
        document.getElementById("ingresso-energia").src = "/images/solar-panel.gif";
        document.getElementById("ingresso-energia-arrow").src = "/images/right-arrow-green.gif";
    } else if (inverterStatus === "Battery Mode" && pvPower === 0) {
        document.getElementById("ingresso-energia").src = "/images/electric-factory.png";
        document.getElementById("ingresso-energia-arrow").src = "/images/right-arrow-stop.png";
    } else {
        document.getElementById("ingresso-energia").src = "/images/electric-factory.png";
        document.getElementById("ingresso-energia-arrow").src = "/images/right-arrow-red.gif";
    }

    // --------------------------- GESTISCO LA BATTERIA ---------------------------

    const batteryArrow = document.getElementById("batteria-arrow");

    // Controlla se l'elemento esiste per evitare errori
    if (!batteryArrow) {
        console.error("Elemento 'batteria-arrow' non trovato!");
    } else {
        const chargingCurrent = batteryChargingCurrent || 0;
        const dischargingCurrent = batteryDischargeCurrent || 0;

        if (inverterStatus === "Battery Mode") {
            if (chargingCurrent > 0) {
                batteryArrow.src = "/images/down-arrow-green.gif";
            } else if (dischargingCurrent > 0) {
                batteryArrow.src = "/images/up-arrow-red.gif";
            } else {
                batteryArrow.src = "/images/up-arrow-stop.png"; // Modalità batteria, ma nessun movimento
            }
        } else {
            batteryArrow.src = "/images/up-arrow-stop.png"; // Non in modalità batteria
        }
    }

    // --------------------------- GESTISCO FRECCIA ABITAZIONE ---------------------------

    if (inverterStatus === "Battery Mode") {
        document.getElementById("fornitura-to-abitazione-arrow").src = "/images/right-arrow-green.gif";
    } else {
        document.getElementById("fornitura-to-abitazione-arrow").src = "/images/right-arrow-red.gif";
    }

    // --------------------------- GESTISCO IL CASO DI FAULT MODE ---------------------------

    if (inverterStatus === "Fault Mode") {
        document.getElementById("inverter").src = "/images/warning.png";
    }

});