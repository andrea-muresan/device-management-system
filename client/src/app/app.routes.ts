import { Routes } from '@angular/router';
import { DeviceInventoryComponent } from './features/device-inventory/device-inventory.component';
import { DeviceDetailsComponent } from './features/device-inventory/device-details/device-details.component';
import { DeviceUpdateComponent } from './features/device-inventory/device-update/device-update.component';

export const routes: Routes = [
    {path: '', component: DeviceInventoryComponent},
    {path: 'inventory', component: DeviceInventoryComponent},
    {path: 'inventory/view/:id', component: DeviceDetailsComponent},
    {path: 'inventory/update/:id', component: DeviceUpdateComponent},
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
