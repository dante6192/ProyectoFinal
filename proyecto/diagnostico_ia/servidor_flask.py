from flask import Flask, request, jsonify
from tensorflow.keras.models import load_model
from tensorflow.keras.preprocessing import image
import numpy as np
import os

app = Flask(__name__)

# Cargar modelo entrenado
modelo = load_model('modelo_diagnostico.keras')
clases = ['cancer_piel', 'lunar_benigno', 'quemadura_solar']  # Asegúrate de que estén en el mismo orden que el entrenamiento

# Diccionario de mensajes interpretativos
mensajes = {
    'cancer_piel': "Se presenta un diagnóstico asociado con cáncer de piel, se recomienda visitar a su médico de confianza.",
    'lunar_benigno': "El lunar parece benigno, pero se recomienda seguimiento periódico con su dermatólogo.",
    'quemadura_solar': "Se detecta quemadura solar, evite la exposición prolongada al sol y use protección solar."
}

@app.route('/analizar-imagen', methods=['POST'])
def analizar_imagen():
    if 'imagen' not in request.files:
        return jsonify({'error': 'No se envió ninguna imagen'}), 400

    img_file = request.files['imagen']
    ruta_temp = os.path.join('temp_img', img_file.filename)
    os.makedirs('temp_img', exist_ok=True)
    img_file.save(ruta_temp)

    try:
        # Procesar imagen
        img = image.load_img(ruta_temp, target_size=(150, 150))
        img_array = image.img_to_array(img) / 255.0
        img_array = np.expand_dims(img_array, axis=0)

        pred = modelo.predict(img_array)
        indice = np.argmax(pred)
        resultado = clases[indice]
        confianza = float(np.max(pred))

        mensaje = mensajes.get(resultado, "Diagnóstico no reconocido, consulte con su médico.")

        return jsonify({
            'resultado': resultado,
            'confianza': round(confianza * 100, 2),
            'mensaje': mensaje
        })
    except Exception as e:
        return jsonify({'error': str(e)}), 500
    finally:
        os.remove(ruta_temp)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
