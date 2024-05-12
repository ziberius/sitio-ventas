import { Directive, Input, ElementRef } from '@angular/core';

@Directive({
  selector: '[appConvertirStock]'
})
export class ConvertirStockDirective {
    @Input('appConvertirStock') value: string = '';

    constructor(private el: ElementRef) {
        let convertedText = '';

        switch (el.nativeElement.innerText) {
            case 'INSTOCK':
                convertedText = 'Disponible';
                break;
            case 'LOWSTOCK':
                convertedText = 'Baja disponibilidad';
                break;
            case 'OUTOFSTOCK':
                convertedText = 'No disponible';
                break;
        }
        this.el.nativeElement.innerText = convertedText;
    }

    ngOnInit() {

    }
}
