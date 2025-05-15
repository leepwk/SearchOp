import { Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';

export const routes: Routes = [
    // Main redirect
    { path: '', redirectTo: 'search', pathMatch: 'full' },
    { path: 'search', component: SearchComponent },  
    // Handle all other routes
  { path: '**', component: SearchComponent }
];
