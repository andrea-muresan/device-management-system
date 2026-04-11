import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Device } from '../../shared/models/device';
import { DeviceDetails } from '../../shared/models/device-details';
import { DeviceSummary } from '../../shared/models/device-summary';
import { DeviceCreate } from '../../shared/models/device-create';
import { Observable } from 'rxjs';
import { AiResponse } from '../../shared/models/AiResponse';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  
  getDevices() {
    return this.http.get<Device[]>(this.baseUrl + 'devices');
  }

  getDeviceDetails(id: number) {
    return this.http.get<DeviceDetails>(this.baseUrl + 'devices/details/' + id);
  }

  getDevicesSummary() {
    return this.http.get<DeviceSummary[]>(this.baseUrl + 'devices/summary');
  }

  createDevice(device: DeviceCreate): Observable<any> {
    return this.http.post(`${this.baseUrl}devices`, device);
  }

  deleteDevice(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}devices/${id}`);
  }

  updateDevice(device: any): Observable<any> {
    return this.http.put(`${this.baseUrl}devices/${device.id}`, device);
  }

  getDeviceById(id: number): Observable<Device> {
    return this.http.get<Device>(`${this.baseUrl}devices/${id}`);
  }

  assignDevice(deviceId: number): Observable<any> {
    return this.http.put(`${this.baseUrl}devices/${deviceId}/assign`, {});
  }

  unassignDevice(deviceId: number): Observable<any> {
    return this.http.put(`${this.baseUrl}devices/${deviceId}/unassign`, {});
  }

  getAiDescription(specs: any): Observable<AiResponse> {
    return this.http.post<AiResponse>(`${this.baseUrl}ai/generate-description`, specs);
  }
}
