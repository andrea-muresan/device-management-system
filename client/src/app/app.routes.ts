import { Routes } from '@angular/router';
import { DeviceInventoryComponent } from './features/device-inventory/device-inventory.component';
import { DeviceDetailsComponent } from './features/device-details/device-details.component';
import { DeviceUpdateComponent } from './features/device-update/device-update.component';
import { DeviceAddComponent } from './features/device-add/device-add.component';

export const routes: Routes = [
    {path: '', component: DeviceInventoryComponent},
    {path: 'inventory', component: DeviceInventoryComponent},
    {path: 'inventory/add', component: DeviceAddComponent},
    {path: 'inventory/view/:id', component: DeviceDetailsComponent},
    {path: 'inventory/update/:id', component: DeviceAddComponent},
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
