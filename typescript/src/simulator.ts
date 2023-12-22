import { CommandSet, CommandData, Orientation } from "./interfaces";
import Robot from "./robot";
import { CommandType } from "./enums";
import {
  PlacementCommand,
  MovementCommand,
  LeftTurnCommand,
  RightTurnCommand,
  StatusCommand,
} from "./commands";

export default class Simulator {
  private _commandLookup: Map<CommandType, CommandSet> = new Map();

  constructor(private _robot: Robot) {
    this.configure();
  }

  public run(commandData: CommandData) {
    const commandSet = this._commandLookup.get(
      <CommandType>commandData.name
    );

    if (commandSet?.enabled) {
      commandSet?.command.execute(
        {
          orientation: <Orientation>commandData.orientation,
        },
        {
          successCallback: commandSet?.handlers?.successCallback,
          failureCallback: commandSet?.handlers?.failureCallback,
        }
      );
    }
  }

  private configure(): void {
    this._commandLookup.set(CommandType.Place, {
      enabled: true,
      command: new PlacementCommand(this._robot),
      handlers: {
        successCallback: () => {
          this.setEnabledCommandExecution(true);
        },
        failureCallback: () => {
          this.setEnabledCommandExecution(false);
        },
      },
    });

    this._commandLookup.set(CommandType.Move, {
      enabled: false,
      command: new MovementCommand(this._robot),
    });

    this._commandLookup.set(CommandType.TurnLeft, {
      enabled: false,
      command: new LeftTurnCommand(this._robot),
    });

    this._commandLookup.set(CommandType.TurnRight, {
      enabled: false,
      command: new RightTurnCommand(this._robot),
    });

    this._commandLookup.set(CommandType.ReportStatus, {
      enabled: false,
      command: new StatusCommand(),
      handlers: {
        successCallback: () => {
          console.log(
            `Output: ${this._robot.getCurrentOrientation().position.x}, ${
              this._robot.getCurrentOrientation().position.y
            }, ${this._robot.getCurrentOrientation().direction}`
          );
        },
      },
    });
  }

  private setEnabledCommandExecution(enabled: boolean) {
    this._commandLookup.forEach(
      (commandSet: CommandSet, commandType: CommandType): void => {
        if (commandType !== CommandType.Place) {
          commandSet.enabled = enabled;
        }
      }
    );
  }
}
