import tensorflow as tf
import numpy as np
from tensorflow.keras.preprocessing import image
import sys

model = tf.keras.models.load_model("modelo_diagnostico.keras")

clases = ['cancer_piel', 'lunar_benigno', 'quemadura_solar']

mensajes = {
    'cancer_piel': "Se presenta un diagnóstico asociado con cáncer de piel, se recomienda visitar a su médico de confianza.",
    'lunar_benigno': "El lunar parece benigno, pero se recomienda seguimiento periódico con su dermatólogo.",
    'quemadura_solar': "Se detecta quemadura solar, evite la exposición prolongada al sol y use protección solar."
}

def predecir(ruta_imagen):
    img = image.load_img(ruta_imagen, target_size=(150,150))
    img_array = image.img_to_array(img)
    img_array = img_array / 255.0
    img_array = np.expand_dims(img_array, axis=0)

    resultado = model.predict(img_array)
    indice = np.argmax(resultado)
    confianza = resultado[0][indice]
    clase = clases[indice]

    print(f"Diagnóstico: {clase} (confianza: {confianza:.2f})")
    print("Mensaje:", mensajes[clase])

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Uso: python predecir_imagen.py ruta_a_imagen.jpg")
    else:
        predecir(sys.argv[1])
