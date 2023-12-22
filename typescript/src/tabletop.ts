import { Dimension, Position } from "./interfaces";
import { PositionConstraint } from "./positionConstraint";

export default class Tabletop {
  private positionConstraint: PositionConstraint;

  constructor(readonly dimension: Dimension) {
    const startingPoint = { x: 0, y: 0 };
    const endingPoint = {
      x: Math.abs(dimension.columns),
      y: Math.abs(dimension.rows),
    };
    this.positionConstraint = new PositionConstraint(
      startingPoint,
      endingPoint
    );
  }

  public contains(position: Position): boolean {
    return this.positionConstraint.contains(position);
  }
}
