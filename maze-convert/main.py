from PIL import Image

# =====================
# CONFIG
# =====================
IMAGE_PATH = "aaa.png"
OUTPUT_PATH = "output.txt"

# Characters from dark → light
CHAR_MAP = [
    ('#', 0, 50),
    ('X', 51, 100),
    ('*', 101, 150),
    ('.', 151, 200),
    (' ', 201, 255),
]

EXPORT_AS = "char_array"  # "char_array" or "string_array"

# =====================
# FUNCTIONS
# =====================
def brightness_to_char(value):
    for char, low, high in CHAR_MAP:
        if low <= value <= high:
            return char
    return ' '

# =====================
# MAIN
# =====================
img = Image.open(IMAGE_PATH).convert("L")  # Convert to grayscale
width, height = img.size
pixels = img.load()

rows = []

for y in range(height):
    row = []
    for x in range(width):
        brightness = pixels[x, y]
        row.append(brightness_to_char(brightness))
    rows.append(row)

# =====================
# EXPORT TO C#
# =====================
with open(OUTPUT_PATH, "w") as f:
    if EXPORT_AS == "char_array":
        f.write("char[,] map = new char[,]\n{\n")
        for row in rows:
            f.write("    { " + ", ".join(f"'{c}'" for c in row) + " },\n")
        f.write("};\n")

    elif EXPORT_AS == "string_array":
        f.write("string[] map = new string[]\n{\n")
        for row in rows:
            f.write(f'    "{"" .join(row)}",\n')
        f.write("};\n")

print("Done! Output written to", OUTPUT_PATH)