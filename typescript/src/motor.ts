import { MotorMovement } from "./enums";

export default class Motor {
  private _currentStep: number = 0;
  private _currentMovement: MotorMovement = MotorMovement.Forward;

  public currentStep(): number {
    return this._currentStep;
  }

  public currentMovement(): MotorMovement {
    return this._currentMovement;
  }

  public step(steps: number): void {
    this._currentStep +=
      this._currentMovement === MotorMovement.Forward ? steps : -steps;
  }

  public changeMovement(mode: MotorMovement): void {
    this._currentMovement = mode;
  }
}
