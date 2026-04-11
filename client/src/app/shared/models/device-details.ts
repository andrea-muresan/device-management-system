import { DeviceType } from "./device-type";

export type DeviceDetails = {
  name: string;
  manufacturer: string;
  type: DeviceType;
  os: string;
  osVersion: string;
  processor: string;
  ram: number;
  description: string;
  userName?: string;
  userRole?: string;
  userLocation?: string;
}