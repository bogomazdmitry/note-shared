import {
  AfterViewInit,
  Directive,
  ElementRef,
  EventEmitter,
  OnDestroy,
  Output,
} from '@angular/core';

@Directive({
  selector: '[domChange]',
})
export class DomChangeDirective implements OnDestroy, AfterViewInit {
  private changes: MutationObserver;

  @Output()
  public domChange = new EventEmitter();

  constructor(private elementRef: ElementRef) {}

  public ngAfterViewInit(): void {
    const element = this.elementRef.nativeElement;

    this.changes = new MutationObserver((mutations: MutationRecord[]) => {
      mutations.forEach((mutation: MutationRecord) => {
        this.domChange.emit(mutation);
      });
    });

    this.changes.observe(element, {
      attributeFilter: ['value', 'list'],
      childList: true,
      characterData: true,
      subtree: true,
    });
  }

  public ngOnDestroy(): void {
    this.changes.disconnect();
  }
}
