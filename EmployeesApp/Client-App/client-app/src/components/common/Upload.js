import { useState, useRef } from "react";
import styles from "./Upload.module.css";

const Upload = (props) => {
  const [selectedFile, setSelectedFile] = useState(null);
  const [error, setError] = useState(false);
  const fileInputRef = useRef();

  const handleFileChange = async (event) => {
    const file = event.target.files[0];

    setSelectedFile(file);
    if (!file.name.endsWith(props.allowedExtension)) {
      setError(true);
      props.uploadFile({ file, error: true });
      return;
    }

    setError(false);
    props.uploadFile({ file, error: false });
  };

  const handleButtonClick = () => {
    fileInputRef.current.click();
  };

  return (
    <div className={styles.upload}>
      <input
        type="file"
        ref={fileInputRef}
        style={{ display: "none" }}
        onChange={handleFileChange}
      />
      <button onClick={handleButtonClick}>Choose File</button>
      {selectedFile && <p>Selected file: {selectedFile.name}</p>}
      {error && (
        <p className={styles.error}>
          Allowed extensions {props.allowedExtension}
        </p>
      )}
    </div>
  );
};

export default Upload;
