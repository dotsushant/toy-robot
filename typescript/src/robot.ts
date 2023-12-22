import { CompassDirection, MotorMovement, TurnDegree } from "./enums";
import { Orientation, Position } from "./interfaces";
import Motor from "./motor";
import Tabletop from "./tabletop";

export default class Robot {
  private _activeMotor: Motor;
  private _verticalMovementMotor: Motor;
  private _horizontalMovementMotor: Motor;

  constructor(readonly tabletop: Tabletop) {
    this._verticalMovementMotor = new Motor();
    this._horizontalMovementMotor = new Motor();
    this._activeMotor = this._verticalMovementMotor;
  }

  public reset(): void {
    this._verticalMovementMotor.changeMovement(MotorMovement.Backward);
    this._horizontalMovementMotor.changeMovement(MotorMovement.Backward);

    while (this._verticalMovementMotor.currentStep() > 0) {
      this._verticalMovementMotor.step(1);
    }

    while (this._horizontalMovementMotor.currentStep() > 0) {
      this._horizontalMovementMotor.step(1);
    }

    while (this.getCurrentDirection() !== CompassDirection.North) {
      this.turnLeft();
    }
  }

  public step(stepCount: number): void {
    if (this.tabletop.contains(this.peekNextPosition())) {
      this._activeMotor.step(stepCount);
    }
  }

  public turnLeft(): void {
    this.turn(TurnDegree.Minus90);
  }

  public turnRight(): void {
    this.turn(TurnDegree.Plus90);
  }

  public getCurrentOrientation(): Orientation {
    return {
      position: this.getCurrentPosition(),
      direction: this.getCurrentDirection(),
    };
  }

  public peekNextPosition(): Position {
    let currentHorizontalStep = this._horizontalMovementMotor.currentStep();
    let currentVerticalStep = this._verticalMovementMotor.currentStep();

    this._activeMotor === this._horizontalMovementMotor
      ? this._activeMotor.currentMovement() === MotorMovement.Backward
        ? --currentHorizontalStep
        : ++currentHorizontalStep
      : this._activeMotor.currentMovement() === MotorMovement.Backward
      ? --currentVerticalStep
      : ++currentVerticalStep;
    return {
      x: currentHorizontalStep,
      y: currentVerticalStep,
    };
  }

  private turn(degree: TurnDegree): void {
    let nextMode;

    if (degree === TurnDegree.Minus90) {
      nextMode =
        this.getCurrentDirection() === CompassDirection.North ||
        this.getCurrentDirection() === CompassDirection.West
          ? MotorMovement.Backward
          : MotorMovement.Forward;
    } else {
      nextMode =
        this.getCurrentDirection() === CompassDirection.North ||
        this.getCurrentDirection() === CompassDirection.West
          ? MotorMovement.Forward
          : MotorMovement.Backward;
    }

    this.switchMotor();
    this._activeMotor.changeMovement(nextMode);
  }

  private switchMotor(): void {
    this._activeMotor =
      this._activeMotor === this._verticalMovementMotor
        ? this._horizontalMovementMotor
        : this._verticalMovementMotor;
  }

  private getCurrentPosition(): Position {
    return {
      x: this._horizontalMovementMotor.currentStep(),
      y: this._verticalMovementMotor.currentStep(),
    };
  }

  private getCurrentDirection(): CompassDirection {
    if (this._activeMotor === this._verticalMovementMotor) {
      return this._activeMotor.currentMovement() === MotorMovement.Forward
        ? CompassDirection.North
        : CompassDirection.South;
    } else if (this._activeMotor === this._horizontalMovementMotor) {
      return this._activeMotor.currentMovement() === MotorMovement.Forward
        ? CompassDirection.East
        : CompassDirection.West;
    } else {
      return CompassDirection.North;
    }
  }
}
