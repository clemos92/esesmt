import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {

  progress: number;
  fileName: string;

  @Input() filePath: string;
  @Input() label: string;
  
  @Output() public onUploadFinished = new EventEmitter();

  constructor(
    private http: HttpClient,
  ) { }

  ngOnInit(): void { }

  get selectedFileName() {
    if (this.filePath) {
      const lastIndexOf = this.filePath.lastIndexOf('\\');
      this.fileName = this.filePath.substr(lastIndexOf + 1);
    }
    return this.fileName;
  }

  public uploadFile(files) {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post(`${environment.apiUrl}/product/UploadImage`, formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.fileName = fileToUpload.name;
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}
