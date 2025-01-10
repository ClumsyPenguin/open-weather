export class Position {
  constructor(longitude: number, latitude: number) {
    this.latitude = latitude;
    this.longitude = longitude;
  }

  readonly longitude!: number;
  readonly latitude!: number;
}
