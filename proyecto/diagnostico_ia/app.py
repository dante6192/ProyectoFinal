from flask import Flask, request, jsonify
from PIL import Image
from transformers import pipeline
import io

app = Flask(__name__)

# Cargar el modelo preentrenado
from transformers import pipeline

classifier = pipeline("image-classification", model="google/vit-base-patch16-224")
@app.route('/')
def home():
    return "API de Diagnóstico por Imágenes funcionando"

@app.route('/analizar-imagen', methods=['POST'])
def analizar_imagen():
    if 'imagen' not in request.files:
        return jsonify({"error": "Imagen no enviada"}), 400

    imagen_file = request.files['imagen']
    imagen_bytes = imagen_file.read()
    imagen = Image.open(io.BytesIO(imagen_bytes)).convert("RGB")

    # Analiza la imagen
    resultado = classifier(imagen)

    return jsonify({"resultado": resultado})

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
