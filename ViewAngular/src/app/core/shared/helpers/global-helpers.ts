import { Injectable } from "@angular/core";



@Injectable({
  providedIn: 'root'
})


export class Helpers {

  public static osMake: boolean;


  constructor() { }

  //#region Generate name for a file that to go upload.
  //Properties and field generateFileName()
  public fileName: string;

  //File to creating in serve
  public _file: File;
  public _files: File[];

  //Methods-----------------------------------------
  //Generate uniq name string using time and date.
  generateFileName(): string {
    var time = new Date();
    //get date to put in name
    var date = new Date();
    //bind itens until to form a full name.
    var dateReplaced = date.toDateString().replace(' ', '.').replace(' ', '.').replace(' ', '.').toLocaleLowerCase();
    //return name of file to upload.
    this.fileName = dateReplaced + '_' + time.getTime() + '.jpg'.toLocaleLowerCase()
    return this.fileName;
  }

  generateSingleFileName(): string {
    var time = new Date();
    //get date to put in name
    var date = new Date();
    //bind itens until to form a full name.
    var dateReplaced = date.toDateString().replace(' ', '.').replace(' ', '.').replace(' ', '.').toLocaleLowerCase();
    //return name of file to upload.
    this.fileName = dateReplaced + '_' + time.getTime() + '.jpg'.toLocaleLowerCase();

    return this.fileName;
  }



  //convert data URI to a blob that can to be saved as a file
  dataURItoBlob(objURI) {
    //EXTRACTED AND ADAPTED
    // convert base64/URLEncoded data component to raw binary data held in a string
    var byteString;
    if (objURI.split(',')[0].indexOf('base64') >= 0) {
      byteString = atob(objURI.split(',')[1]);
    }
    else {
      byteString = decodeURI(objURI.split(',')[1]);
    }
    // separate out the mime component
    var mimeString = objURI.split(',')[0].split(':')[1].split(';')[0];
    // write the bytes of the string to a typed array
    var ia = new Uint8Array(byteString.length);
    for (var i = 0; i < byteString.length; i++) {
      ia[i] = byteString.charCodeAt(i);
    }
    return new Blob([ia], { type: mimeString });
  }
  //#endregion




  //#endregion

}






