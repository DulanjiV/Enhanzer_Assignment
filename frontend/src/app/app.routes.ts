import { Routes } from '@angular/router';
import { BookListComponent } from './components/book-list/book-list.component';

export const routes: Routes = [
    { path: '', redirectTo: 'book-list', pathMatch: 'full' },
    { path: 'book-list', component: BookListComponent }
];