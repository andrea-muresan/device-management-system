import { Routes } from '@angular/router';
import { DeviceInventoryComponent } from './features/device-inventory/device-inventory.component';
import { DeviceDetailsComponent } from './features/device-details/device-details.component';
import { DeviceAddComponent } from './features/device-add/device-add.component';
import { LoginComponent } from './features/account/login/login.component';
import { RegisterComponent } from './features/account/register/register.component';

export const routes: Routes = [
    {path: '', component: LoginComponent},
    {path: 'inventory', component: DeviceInventoryComponent},
    {path: 'account/login', component: LoginComponent},
    {path: 'account/register', component: RegisterComponent},
    {path: 'inventory/add', component: DeviceAddComponent},
    {path: 'inventory/view/:id', component: DeviceDetailsComponent},
    {path: 'inventory/update/:id', component: DeviceAddComponent},
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
