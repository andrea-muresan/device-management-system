import { DeviceType } from "./device-type";

export type DeviceSummary = {
    id: number;
    name: string;
    type: DeviceType;
    os: string;
    userName?: string;
    userLocation?: string;
}