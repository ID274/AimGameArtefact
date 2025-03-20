import pandas as pd
import glob
from pathlib import Path

# Define the base directory for data files
base_dir = Path("data")

# Define subdirectories for shots and summary files
shots_dir = base_dir / "shots"
summary_dir = base_dir / "summary"

# Function to extract player and run from filename
def extract_player_run(filename):
    parts = filename.stem.split('_')
    player = parts[0]
    run = parts[1]
    return player, run

# Function to read individual shot data from a single file
def read_shot_data(file_path):
    player, run = extract_player_run(file_path)
    data = []
    with open(file_path, 'r') as file:
        segments = file.read().split('\n\n')  # Split segments by double newlines
        for segment in segments:
            if segment.strip():  # Skip empty segments
                try:
                    lines = segment.strip().split('\n')
                    record = {
                        "Player": player,
                        "Run": run,
                        "Shot number": int(lines[0].split(": ")[1].strip().strip(',')),
                        "Hit": lines[1].split(": ")[1].strip().strip(',') == 'True',
                        "Time since last shot": float(lines[2].split(": ")[1].strip().strip(',')),
                        "Mouse Sensitivity": float(lines[3].split(": ")[1].strip().strip(',')),
                        "Crosshair Type": lines[4].split(": ")[1].strip().strip(','),
                        "Crosshair Size": float(lines[5].split(": ")[1].strip().strip(',')),
                        "Camera FOV": int(lines[6].split(": ")[1].strip().strip(','))
                    }
                    data.append(record)
                except (IndexError, ValueError) as e:
                    print(f"Error processing file {file_path}: {e}")
                    print(f"Segment content: {segment}")
    return data

# Function to read summary data from a single file
def read_summary_data(file_path):
    player, run = extract_player_run(file_path)
    summary_data = {"Player": player, "Run": run}
    with open(file_path, 'r') as file:
        lines = file.readlines()
        for line in lines:
            try:
                key, value = line.split(": ")
                key = key.strip().strip(',')
                value = value.strip().strip(',')
                if key in ["Total shots", "Total score", "Camera FOV"]:
                    summary_data[key] = int(value)
                elif key in ["Mouse sensitivity", "Average accuracy", "Average time since last shot", "Crosshair Size"]:
                    summary_data[key] = float(value)
                else:
                    summary_data[key] = value
            except ValueError as e:
                print(f"Error processing file {file_path}: {e}")
                print(f"Line content: {line}")
    return summary_data

# Read shot data from all files in the directory
shot_file_paths = glob.glob(str(shots_dir / "*.txt"))
all_shot_data = []
for file_path in shot_file_paths:
    all_shot_data.extend(read_shot_data(Path(file_path)))

# Read summary data from all files in the directory
summary_file_paths = glob.glob(str(summary_dir / "*.txt"))
all_summary_data = []
for file_path in summary_file_paths:
    all_summary_data.append(read_summary_data(Path(file_path)))

# Convert data to DataFrame
shot_df = pd.DataFrame(all_shot_data)
summary_df = pd.DataFrame(all_summary_data)

# Save DataFrame to Excel
with pd.ExcelWriter("experiment_data.xlsx") as writer:
    shot_df.to_excel(writer, sheet_name="Shot Data", index=False)
    summary_df.to_excel(writer, sheet_name="Summary Data", index=False)

print("Data has been written to experiment_data.xlsx")
