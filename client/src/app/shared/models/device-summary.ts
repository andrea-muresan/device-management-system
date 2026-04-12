import { DeviceType } from "./device-type";

export type DeviceSummary = {
    id: number;
    name: string;
    type: DeviceType;
    manufacturer: string;
    processor: string;
    ram:number;
    userName?: string;
}