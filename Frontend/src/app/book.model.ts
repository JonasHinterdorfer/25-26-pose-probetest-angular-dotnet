export interface BookDto extends BookCreateDto{
  id:number;
}
export interface BookCreateDto{
  title : string,
  description : string,
  author: string,
  publishedDate : string,
  price : number,
  isAvailable : boolean,
}
