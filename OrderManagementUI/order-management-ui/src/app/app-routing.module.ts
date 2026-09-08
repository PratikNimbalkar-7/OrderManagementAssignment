import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderComponent } from './Components/order/order.component';
import { ViewOrderComponent } from './Components/view-order/view-order.component';
import { HomeComponent } from './Components/home/home.component';

const routes: Routes = [

  {
    path: '',
    component: HomeComponent
  },
  {
    path:'order',
    component: OrderComponent
  },
  {
    path: 'view-order',
    component: ViewOrderComponent
  },
  {
    path: '**',
    redirectTo: ''
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
