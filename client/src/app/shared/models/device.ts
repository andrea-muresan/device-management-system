import { DeviceType } from "./device-type";

export type Device = {
    id: number; 
    name: string;
    manufacturer: string;
    type: DeviceType;
    os: string;
    osVersion: string;
    processor: string;
    ram: number;
    description: string;
    userId?: number;
}