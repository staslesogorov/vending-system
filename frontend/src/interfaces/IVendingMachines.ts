export default interface IVendingMachines {
    id: string;
    location: string;
    model: string;
    type: string;
    totalRevenue: number;

    serialNumber: string;
    inventoryNumber: string;

    manufacturer: string;
    manufactureDate: string;          // DateTime
    commissioningDate: string;        // DateTime
    lastCalibrationDate: string;       // DateTime
    calibrationIntervalMonths: number;
    resourceHours: number;
    nextMaintenanceDate: string;       // DateTime
    maintenanceDurationHours: number;
    status: string;
    productionCountry: string;
    inventoryDate: string;             // DateTime
}