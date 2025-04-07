// EncryptionService.ts
import { JSEncrypt } from 'jsencrypt';
import { IEncryptionService } from './IEncryptionService';

export class RsaEncryptionService implements IEncryptionService {
  private readonly publicKey: string;

  constructor() {
    const publicKey = process.env.REACT_APP_RSA_PUBLIC_KEY?.replace(/\n/g, '');
    if (!publicKey) {
      throw new Error('Public key is missing from env');
    }

    this.publicKey = publicKey; 
  }

  encrypt(plainText: string): string {
    const encryptor = new JSEncrypt();
    encryptor.setPublicKey(this.publicKey);

    const encrypted = encryptor.encrypt(plainText);
    if (!encrypted) throw new Error('RSA encryption failed');
    return encrypted;
  }

  decrypt(): never {
    throw new Error('Decrypt is not available on frontend');
  }
}
