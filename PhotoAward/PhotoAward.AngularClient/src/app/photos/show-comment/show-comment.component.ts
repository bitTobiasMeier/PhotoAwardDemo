import { CommentData } from './../../Shared/Controllers.generated';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'pac-show-comment',
  templateUrl: './show-comment.component.html',
  styles: []
})
export class ShowCommentComponent implements OnInit {
 @Input() comment: CommentData;
  constructor() { }

  ngOnInit() {
  }

}
