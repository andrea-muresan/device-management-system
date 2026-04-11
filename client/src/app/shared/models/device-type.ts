export enum DeviceType {
    Phone = 0,
    Tablet = 1
}

export const DEVICE_TYPE_LIST = [
  { id: DeviceType.Phone, name: 'Phone' },
  { id: DeviceType.Tablet, name: 'Tablet' }
] as const;