import { NgModule } from '@angular/core';
import { MatButtonModule, MatRadioModule, MatFormFieldModule, MatExpansionModule, MatProgressSpinnerModule, MatTableModule, MatPaginatorModule, MatSortModule } from '@angular/material';
import { FieldsetModule } from 'primeng/fieldset';
import { DropdownModule } from 'primeng/dropdown';
import { AccordionModule } from 'primeng/accordion';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { PaginatorModule } from 'primeng/paginator';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [

    MatButtonModule, MatRadioModule, MatFormFieldModule, MatExpansionModule, MatProgressSpinnerModule, MatTableModule, MatPaginatorModule, MatSortModule,
    FieldsetModule, DropdownModule, AccordionModule, PanelModule, TableModule, PaginatorModule, ToastModule,
    FormsModule, ReactiveFormsModule
  ],
  exports: [
    MatButtonModule, MatRadioModule, MatFormFieldModule, MatExpansionModule, MatProgressSpinnerModule, MatTableModule, MatPaginatorModule, MatSortModule,
    FieldsetModule, DropdownModule, AccordionModule, PanelModule, TableModule, PaginatorModule, ToastModule,
    FormsModule, ReactiveFormsModule]
})
export class ImportModule {

}
