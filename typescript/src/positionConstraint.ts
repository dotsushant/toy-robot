import { Position } from "./interfaces";

export class PositionConstraint {
  static create(start: Position, end: Position): PositionConstraint {
    return new PositionConstraint(start, end);
  }

  constructor(private _lowerBound: Position, private _upperBound: Position) {}

  public contains(position: Position): boolean {
    const lower =
      this._lowerBound.y <= position.y && this._lowerBound.x <= position.x;
    const upper =
      this._upperBound.y > position.y && this._upperBound.x > position.x;
    return lower && upper;
  }
}
