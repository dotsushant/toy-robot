import fs from "fs";
import readline from "readline";
import commandParser from "commander";
import { CompassDirection } from "./enums";
import Simulator from "./simulator";
import Robot from "./robot";
import Tabletop from "./tabletop";

class App {
  private simulator: Simulator = new Simulator(
    new Robot(
      new Tabletop({
        rows: 5,
        columns: 5,
      })
    )
  );

  constructor() {
    commandParser
      .arguments("[filename]")
      .description("Test robot using file data or user input.", {
        filename: "File where test data is stored",
      })
      .action(async (filename: string) => {
        if (filename) {
          await this.startAutomatedMode(filename);
        } else {
          this.startInteractiveMode();
        }
      });
  }

  public start(): void {
    console.clear();
    commandParser.parse(process.argv);
  }

  private async startAutomatedMode(filename: string) {
    const fileStream = fs.createReadStream(filename);

    const terminal = readline.createInterface({
      input: fileStream,
      crlfDelay: Infinity,
    });

    for await (const line of terminal) {
      console.log(line);
      this.processCommand(line);
    }
  }

  private startInteractiveMode() {
    console.log('Welcome to the Toy robot simulator (start typing commands below)');
    const terminal = readline.createInterface({
      input: process.stdin,
      output: process.stdout,
      terminal: false,
    });

    const prompt = () => {
      terminal.question("", (command: string) => {
        this.processCommand(command);
        prompt();
      });
    };

    prompt();
  }

  private processCommand(command: string): void {
    const placeCommandParsingExpression = /^(PLACE) (\d+),(\d+),(SOUTH|NORTH|WEST|EAST)$/gm;
    const placeCommandParameters = placeCommandParsingExpression.exec(command);

    const otherCommandParsingExpression = /^(MOVE|LEFT|RIGHT|REPORT)$/gm;
    const otherCommandParameters = otherCommandParsingExpression.exec(command);

    if (placeCommandParameters) {
      this.simulator.run({
        name: placeCommandParameters[1],
        orientation: {
          position: {
            x: Number(placeCommandParameters[2]),
            y: Number(placeCommandParameters[3]),
          },
          direction: <CompassDirection>placeCommandParameters[4],
        },
      });
    } else if (otherCommandParameters) {
      this.simulator.run({
        name: otherCommandParameters[1],
      });
    }
  }
}

const app = new App();
app.start();
